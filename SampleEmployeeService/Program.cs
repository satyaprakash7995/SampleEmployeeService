using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SampleEmployeeService.ApplicationLayer.Extensions;
using SampleEmployeeService.Infrastructure.Extensions;
using SampleEmployeeService.Infrastructure.Persistence;
using SampleEmployeeService.Profiles;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args); ;

// Add services to the container.

builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<DtoMappingProfile>();
});

builder.Services.AddDbContext<SampleEmployeeServiceDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), x =>
    {
        x.MigrationsAssembly("SampleEmployeeService.Infrastructure");
        x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
    });
});


builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SampleEmployeeService", Version = "v1" });
});

builder.Services.AddCors(o =>
{
    o.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

var app = builder.Build();

IConfiguration configuration = app.Configuration;
IWebHostEnvironment environment = app.Environment;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "/swagger/{documentName}/swagger.json";
    });

    app.UseSwaggerUI(c => c.SwaggerEndpoint($"/swagger/v1/swagger.json", "SampleEmployeeServiceApi v1"));
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();