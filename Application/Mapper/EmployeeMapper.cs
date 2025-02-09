using System.Runtime.CompilerServices;

public static class EmployeeMapper
{
    public static EmployeeDto ToDto(this Employee employee)
    {
        return new EmployeeDto
        {
            Id = employee.EmployeeId,
            Name = employee.EmployeeName,
            Salary = employee.Salary,
        };
    }
}
