using MediatR;

public class GetEmployeeQueryHandler(IEmployeeRepository employeeRepository) : IRequestHandler<GetEmployeeQuery, PaginatedResult<IEnumerable<EmployeeDto>>>
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    public async Task<PaginatedResult<IEnumerable<EmployeeDto>>> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
    {
        return await _employeeRepository.GetAllEmployeesAsync(request.Filter);
    }
}
