using MediatR;

public class GetEmployeeQueryHandler(IEmployeeRepository employeeRepository) : IRequestHandler<GetEmployeeQuery, PaginatedResult<IEnumerable<EmployeeDto>>>
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    public Task<PaginatedResult<IEnumerable<EmployeeDto>>> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
