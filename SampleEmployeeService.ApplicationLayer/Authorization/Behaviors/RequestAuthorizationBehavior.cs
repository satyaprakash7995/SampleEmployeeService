using SampleEmployeeService.ApplicationLayer.Authorization.Exceptions;
using SampleEmployeeService.ApplicationLayer.Authorization.Interfaces;
using SampleEmployeeService.ApplicationLayer.Authorization.Models;
using MediatR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SampleEmployeeService.ApplicationLayer.Authorization.Behaviors
{
    public class RequestAuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private static readonly ConcurrentDictionary<Type, Type> RequirementHandlers = new();
        private static readonly ConcurrentDictionary<Type, MethodInfo> HandlerMethodInfo = new();

        private readonly IEnumerable<IAuthorizer<TRequest>> _authorizers;

        private readonly IServiceProvider _serviceProvider;

        public RequestAuthorizationBehavior(IEnumerable<IAuthorizer<TRequest>> authorizers,
            IServiceProvider serviceProvider)
        {
            _authorizers = authorizers;
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var requirements = new HashSet<IAuthorizationRequirement>();

            foreach (var authorizer in _authorizers)
            {
                authorizer.BuildPolicy(request);
                foreach (var requirement in authorizer.Requirements)
                    requirements.Add(requirement);
            }

            foreach (var requirement in requirements)
            {
                var result = await ExecuteAuthorizationHandler(requirement, cancellationToken);

                if (!result.IsAuthorized)
                    throw new UnauthorizedException(result.FailureMessage);
            }

            return await next();
        }

        private Task<AuthorizationResult> ExecuteAuthorizationHandler(IAuthorizationRequirement requirement,
            CancellationToken cancellationToken)
        {
            var requirementType = requirement.GetType();
            var handlerType = FindHandlerType(requirement);

            if (handlerType == null)
                throw new InvalidOperationException(
                    $"Could not find an authorization handler type for requirement type \"{requirementType.Name}\"");

            var handlersEnumerable =
                _serviceProvider.GetService(typeof(IEnumerable<>).MakeGenericType(handlerType)) as IEnumerable<object>;

            var handlers = handlersEnumerable.ToList();
            if (handlersEnumerable == null || !handlers.Any())
                throw new InvalidOperationException(
                    $"Could not find an authorization handler implementation for requirement type \"{requirementType.Name}\"");

            if (handlers.Count > 1)
                throw new InvalidOperationException(
                    $"Multiple authorization handler implementations were found for requirement type \"{requirementType.Name}\"");

            var serviceHandler = handlers.First();
            var serviceHandlerType = serviceHandler.GetType();

            var methodInfo = HandlerMethodInfo.GetOrAdd(
                serviceHandlerType,
                serviceHandlerType
                    .GetMethods()
                    .FirstOrDefault(x => x.Name == nameof(IAuthorizationHandler<IAuthorizationRequirement>.Handle)));

            return (Task<AuthorizationResult>)methodInfo.Invoke(serviceHandler,
                new object[] { requirement, cancellationToken });
        }

        private static Type FindHandlerType(IAuthorizationRequirement requirement)
        {
            var requirementType = requirement.GetType();
            var handlerType = RequirementHandlers.GetOrAdd(requirementType,
                requirementTypeKey =>
                {
                    var wrapperType = typeof(IAuthorizationHandler<>).MakeGenericType(requirementTypeKey);

                    return wrapperType;
                });

            return handlerType;
        }
    }
}
