using MediatR;
using System.ComponentModel.DataAnnotations;

public record UpdateEmployeeCommand(UpdateEmployeeDto Employee) : IRequest<CommandResponse>;
