using SampleEmployeeService.ApplicationLayer.Common.Interfaces.Contexts;
using SampleEmployeeService.ApplicationLayer.Common.Interfaces.Services;
using SampleEmployeeService.Domain.Entities;
using SampleEmployeeService.Domain.Entities.Common;
using SampleEmployeeService.Domain.Identity;
using SampleEmployeeService.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace SampleEmployeeService.Infrastructure.Persistence
{
    public class SampleEmployeeServiceDbContext 
        : IdentityDbContext<ApplicationUser, ApplicationRole, string>, ISampleEmployeeServiceDbContext
    {

        private readonly ICurrentUserService _authenticatedUser;

        private readonly IDateTimeService _dateTime;

        
        public SampleEmployeeServiceDbContext(DbContextOptions<SampleEmployeeServiceDbContext> options) : base(options)
        {
            

        }

        public SampleEmployeeServiceDbContext(DbContextOptions<SampleEmployeeServiceDbContext> options,
            IDateTimeService dateTime,
            ICurrentUserService authenticatedUser) : base(options)
        {
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
        }

        public IDbConnection Connection => Database.GetDbConnection();

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdatedAuditEntities();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            UpdatedAuditEntities();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChanges)
        {
            UpdatedAuditEntities();
            return base.SaveChanges(acceptAllChanges);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChanges, CancellationToken cancellationToken)
        {
            UpdatedAuditEntities();
            return await base.SaveChangesAsync(acceptAllChanges, cancellationToken);
        }

        internal static SampleEmployeeServiceDbContext CreateContext()
        {
            return new SampleEmployeeServiceDbContext(new DbContextOptionsBuilder<SampleEmployeeServiceDbContext>()
                .UseSqlServer(new ConfigurationBuilder()
                    .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"))
                    .AddUserSecrets(Assembly.GetExecutingAssembly())
                    .AddEnvironmentVariables()
                    .Build()
                    .GetConnectionString("DefaultConnection")).Options);          
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(SampleEmployeeServiceDbContext).Assembly);

            foreach (var property in builder.Model.GetEntityTypes()
                         .SelectMany(x => x.GetProperties())
                         .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?))
                    )
                property.SetColumnType("decimal(18,2)");

            base.OnModelCreating(builder);

            builder.HasDefaultSchema("dbo");

            builder.Entity<ApplicationUser>(entity => { entity.ToTable("Users", "Identity"); });
            builder.Entity<ApplicationRole>(entity => { entity.ToTable("Roles", "Identity"); });
            builder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable("UserRoles", "Identity"); });
            builder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable("UserClaims", "Identity"); });
            builder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable("UserLogins", "Identity"); });
            builder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable("RoleClaims", "Identity"); });
            builder.Entity<IdentityUserToken<string>>(entity => { entity.ToTable("UserTokens", "Identity"); });
        }

        private void UpdatedAuditEntities()
        {
            var modifiedEntities = ChangeTracker
                .Entries()
                .Where(x => x.Entity is IAuditBaseEntity && x.State is EntityState.Added or EntityState.Modified);

            foreach (var entry in modifiedEntities)
            {
                var entity = (IAuditBaseEntity)entry.Entity;
                var now = _dateTime.UtcNow;
                if (entry.State == EntityState.Added)
                {
                    entity.CreatedDate = now;
                    entity.CreatedBy = _authenticatedUser.UserId;
                }
                else
                {
                    base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                    base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                }

                entity.UpdatedDate = _dateTime.UtcNow;
                entity.UpdatedBy = _authenticatedUser.UserId;
            }
        }
    }
}
