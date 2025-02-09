using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<CommandResponse> CreateEmployee([FromForm] CreateEmployeeDto employee)
    {
        CommandResponse response = new();

        CreateEmployeeCommand command = new(employee);
        var result = await _mediator.Send(command);

        if(result.Success == false)
            Response.StatusCode = StatusCodes.Status400BadRequest;

        response.Success = result.Success;
        response.Message = result.Message;
        response.Errors = result.Errors;
        return response;
    }


    [HttpPut]
    public async Task<CommandResponse> UpdateEmployee([FromForm] UpdateEmployeeCommand command)
    {
        CommandResponse response = new();
        var result = await _mediator.Send(command);

        if (result.Success == false)
            Response.StatusCode = StatusCodes.Status400BadRequest;

        response.Message = result.Message;
        response.Errors = result.Errors;
        return response;
    }

    [HttpGet]
    public async Task<PaginatedResult<IEnumerable<EmployeeDto>>> GetEmployee([FromQuery] EmployeeFilter filter)
    {
        GetEmployeeQuery query = new(filter);
        return await _mediator.Send(query);
    }
}
