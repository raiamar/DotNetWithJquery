using Microsoft.Data.SqlClient;
using System.Data;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly string _connectionString;
    public EmployeeRepository(IDbConnection dbConnection)
    {
        _connectionString = dbConnection.GetConnectionString();
    }
    public async Task<int> CreateEmployeeAsync(CreateEmployeeDto employee)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            using(var command = new SqlCommand("SP_CreateEmployee", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@EmployeeName", SqlDbType.NVarChar, 250).Value = employee.Name;
                command.Parameters.Add("@Salary", SqlDbType.Decimal).Value = employee.Salary;
                var paramId = command.Parameters.Add("@EmployeeId", SqlDbType.Int);

                paramId.Direction = ParameterDirection.Output;
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                return (int)paramId.Value;
            }
        }
    }



    public async Task<bool> UpdateEmployeeSalaryAsync(UpdateEmployeeDto employee)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("SP_UpdateEmployeeSalary", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@EmployeeId", SqlDbType.Int).Value = employee.Id;
                command.Parameters.Add("@Salary", SqlDbType.Decimal).Value = employee.Salary;

                await connection.OpenAsync();
                var result = await command.ExecuteNonQueryAsync();
                return result > 0;
            }
        }
    }
}
