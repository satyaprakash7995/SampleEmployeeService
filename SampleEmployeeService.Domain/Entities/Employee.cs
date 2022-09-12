using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SampleEmployeeService.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEmployeeService.Domain.Entities
{
    [Table("Employees")]
    public class Employee : AuditBaseEntity
    {
        public int EmployeeId { get; set; }

        [StringLength(30)]
        public string FirstName { get; set; }
        [StringLength(20)]
        public string MiddleName { get; set; }

        [StringLength(20)]
        public string LastName { get; set; }
    }
}
