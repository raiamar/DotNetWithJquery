public interface IEmployeeRepository
{
    Task<int> CreateEmployeeAsync(CreateEmployeeDto employee);
}
