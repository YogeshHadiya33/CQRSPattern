using CQRSPattern.API.Contracts.V1;
using CQRSPattern.Entity.Employee.Bussiness;
using CQRSPattern.Services.Features.Employees.Command.Create;
using CQRSPattern.Services.Features.Employees.Command.Delete;
using CQRSPattern.Services.Features.Employees.Command.Get;
using CQRSPattern.Services.Features.Employees.Command.GetAll;
using CQRSPattern.Services.Features.Employees.Command.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CQRSPattern.API.Controllers;

[Authorize]
public class EmployeeController : BaseController
{
    private readonly IMediator _mediator;

    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route(APIRoutes.Employee.GetAll)]
    public async Task<IActionResult> GetAllEmployees()
    {
        var userId = User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var result = await _mediator.Send(new GetAllEmployeeCommand());
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    [Route(APIRoutes.Employee.Get)]
    public async Task<IActionResult> GetEmployee(int id)
    {
        var query = new GetEmployeeCommand { Id = id };
        var result = await _mediator.Send(query);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost]
    [Route(APIRoutes.Employee.Create)]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeModel employee)
    {
        var result = await _mediator.Send(new CreateEmployeeCommand()
        {
            UserId = UserId,
            Employee = employee
        });
        return StatusCode(result.StatusCode, result);
    }

    [HttpPut]
    [Route(APIRoutes.Employee.Update)]
    public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeModel employee)
    {
        var result = await _mediator.Send(new UpdateEmployeeCommand
        {
            UserId = UserId,
            Employee = employee
        });
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete]
    [Route(APIRoutes.Employee.Delete)]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var command = new DeleteEmployeeCommand { Id = id };
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }
}