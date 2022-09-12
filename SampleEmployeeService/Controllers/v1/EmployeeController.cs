using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleEmployeeService.ApplicationLayer.Features.Employees.Commands.CreateEmployee;
using SampleEmployeeService.ApplicationLayer.Features.Employees.Commands.DeleteEmployee;
using SampleEmployeeService.ApplicationLayer.Features.Employees.Queries.GetEmployeeById;
using SampleEmployeeService.ApplicationLayer.Features.Employees.Queries.GetEmployeeList;
using SampleEmployeeService.Controllers.Models;
using SampleEmployeeService.Domain.ResponseResult;

namespace SampleEmployeeService.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseApiController<EmployeeController>
    {
                
        [HttpGet]
        [Route("{employeeId}")]
        public async Task<IActionResult> GetEmployee([FromQuery] int employeeId)
        {
            var result = await Mediator.Send(new GetEmployeeByIdCommand
            {
                EmployeeId = employeeId
            });
            if (result.Data == null)
            {
                return NotFound();
            }
            return Ok(result.Data);
        }

        [HttpGet]
        [Route("/api/Employee/GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await Mediator.Send(new GetEmployeeListCommand()
                    {
                       
                    });
                    if (result.Data == null)
                    {
                        return NotFound();
                    }
                    return Ok(result.Data);
                }

            }
            catch (Exception ex)
            {
                return BadRequest("Error ");
            }
            return null;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Post([FromBody] EmployeeVm employee)
        
        {
            BaseResponseResult<CreateOrUpdateEmployeeDto> result;
            try
            {
                if (ModelState.IsValid)
                {
                    var emp = Mapper.Map<CreateOrUpdateEmployeeDto>(employee);
                    result = await Mediator.Send(new CreateOrUpdateEmployeeCommand
                    {
                        CreateEmployeeDto = emp
                    });
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error ");
            }
            return null;
        }

        [HttpPut("{employeeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put([FromBody] EmployeeVm employee)
        {
            var emp = Mapper.Map<CreateOrUpdateEmployeeDto>(employee);
            var result = await Mediator.Send(new CreateOrUpdateEmployeeCommand
            {
                CreateEmployeeDto = emp
            });
            return NoContent();
        }

        [HttpDelete("{employeeId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(int employeeId)
        {
            var result = await Mediator.Send(new DeleteEmployeeCommand
            {
                EmployeeId = employeeId
            });
            return NoContent();
        }

        private void AddError(IEnumerable<string> errors, string key = "")
        {
            foreach (var error in errors)
            {
                AddError(error, key);
            }
        }

        private void AddError(string error, string key = "")
        {
            ModelState.AddModelError(key, error);
        }

    }
}
