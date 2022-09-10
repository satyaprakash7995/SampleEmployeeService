using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEmployeeService.Domain.Constants
{
    public static class Constants
    {
        public const string Title = "Anthony Travel Portal";
        public static readonly IReadOnlyCollection<string> AdminRoles = new[] { "Admin" };
    }
}
