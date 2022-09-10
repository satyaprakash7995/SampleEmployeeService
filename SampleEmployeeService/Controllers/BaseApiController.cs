using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SampleEmployeeService.Domain.ResponseResult;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;

namespace SampleEmployeeService.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseApiController<T> : ControllerBase
    {
        private ILogger<T> _loggerInstance;
        private IMapper _mapperInstance;
        private IMediator _mediatorInstance;


        protected BaseApiController()
        {
        }
        protected BaseApiController(ILogger<T> loggerInstance, IMapper mapperInstance,
       IMediator mediatorInstance)
        {
            _loggerInstance = loggerInstance;
            _mapperInstance = mapperInstance;
            _mediatorInstance = mediatorInstance;
        }
        public string UserId => HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        public string Username => HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

        protected IMapper Mapper => _mapperInstance ??= HttpContext.RequestServices.GetService<IMapper>();
        protected IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();
        protected ILogger<T> Logger => _loggerInstance ??= HttpContext.RequestServices.GetService<ILogger<T>>();
        protected bool SetModelErrors<TR>(BaseResponseResult<TR> model)
        {
            if (model.Succeeded)
                return false;

            ModelState.AddModelError("Error", "An error occured.");
            model.ValidationResult.Errors.ForEach(e => { ModelState.AddModelError(e.PropertyName, e.ErrorMessage); });
            return true;
        }
        protected string RequestBody()
        {
            var bodyStream = new StreamReader(Request.Body);
            if (!bodyStream.BaseStream.CanSeek)
                return null;
            bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
            var bodyText = bodyStream.ReadToEnd();
            bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
            return bodyText;
        }
    }
}
