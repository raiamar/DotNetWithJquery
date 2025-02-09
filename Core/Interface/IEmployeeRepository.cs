public interface IEmployeeRepository
{
    Task<int> CreateEmployeeAsync(CreateEmployeeDto employee);
    Task<bool> UpdateEmployeeSalaryAsync(UpdateEmployeeDto employee);
    Task<PaginatedResult<IEnumerable<EmployeeDto>>> GetAllEmployeesAsync(EmployeeFilter filter);
}
