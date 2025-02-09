using MediatR;

public record GetEmployeeQuery(EmployeeFilter Filter) : IRequest<PaginatedResult<IEnumerable<EmployeeDto>>>;
