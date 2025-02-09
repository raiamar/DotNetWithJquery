using MediatR;

public record CreateEmployeeCommand(CreateEmployeeDto Employee) : IRequest<CommandResponse>;