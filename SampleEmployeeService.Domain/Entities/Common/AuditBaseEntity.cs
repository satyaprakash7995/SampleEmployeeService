using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEmployeeService.Domain.Entities.Common
{
    public abstract class AuditBaseEntity : IAuditBaseEntity, IBaseEntity
    {
        public DateTimeOffset CreatedDate { get; set; }

        [StringLength(128)]
        public string CreatedBy { get; set; }

        public DateTimeOffset? UpdatedDate { get; set; }

        [StringLength(128)]
        public string UpdatedBy { get; set; }
    }
}
