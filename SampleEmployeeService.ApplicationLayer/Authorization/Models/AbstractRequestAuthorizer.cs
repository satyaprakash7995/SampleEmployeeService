using SampleEmployeeService.ApplicationLayer.Authorization.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEmployeeService.ApplicationLayer.Authorization.Models
{
    public abstract class AbstractRequestAuthorizer<TRequest> : IAuthorizer<TRequest>
    {
        private readonly HashSet<IAuthorizationRequirement> _requirements = new();

        public IEnumerable<IAuthorizationRequirement> Requirements => _requirements;

        public abstract void BuildPolicy(TRequest request);

        protected void UseRequirement(IAuthorizationRequirement requirement)
        {
            if (requirement == null) return;
            _requirements.Add(requirement);
        }
    }
}
