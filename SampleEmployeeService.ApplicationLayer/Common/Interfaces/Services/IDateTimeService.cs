using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEmployeeService.ApplicationLayer.Common.Interfaces.Services
{
    public interface IDateTimeService
    {
        DateTimeOffset UtcNow { get; }
    }
}
