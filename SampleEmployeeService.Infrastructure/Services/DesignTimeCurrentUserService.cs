using SampleEmployeeService.ApplicationLayer.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SampleEmployeeService.Infrastructure.Services
{
    public class DesignTimeCurrentUserService : ICurrentUserService
	{
		public ClaimsPrincipal User => throw new NotImplementedException("Not available on design time");
		public string UserId => "DesignTime";
		public string Username => "DesignTime";
		
		public bool IsAdmin()
		{
			throw new NotImplementedException("Not available on design time");
		}
	}
}
