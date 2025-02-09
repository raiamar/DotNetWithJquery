public interface IEmployeeRepository
{
    Task<int> CreateEmployeeAsync(CreateEmployeeDto employee);
    Task<bool> UpdateEmployeeSalaryAsync(UpdateEmployeeDto employee);
}
