using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEmployeeService.Domain.Entities.Common
{
    public interface IAuditBaseEntity
    {
        DateTimeOffset CreatedDate { get; set; }
        string CreatedBy { get; set; }
        DateTimeOffset? UpdatedDate { get; set; }
        string UpdatedBy { get; set; }
    }
}
