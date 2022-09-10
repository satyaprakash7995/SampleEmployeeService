using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEmployeeService.Domain.Enums
{
    public enum UserType
    {
        [Display(Name = "Athletics Business offices")]
        Athletics = 1,
        [Display(Name = "Procurement/Travel offices")]
        UniversityBusinessTravel = 2,
        [Display(Name = "Internal Users")]
        InternalUsers = 3
    }
}
