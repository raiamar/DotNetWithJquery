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



    public async Task<PaginatedResult<IEnumerable<EmployeeDto>>> GetAllEmployeesAsync(EmployeeFilter filter)
    {
        var employees = new List<EmployeeDto>();
        int totalEmployees = 0;

        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("SP_GetAllEmployees", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters for pagination and sorting
                command.Parameters.AddWithValue("@PageNumber", filter.Page);
                command.Parameters.AddWithValue("@PageSize", filter.PageSize);
                command.Parameters.AddWithValue("@SortOrder", filter.SortOrder);

                // Output parameter for total employee count
                var totalEmployeesParam = new SqlParameter("@TotalEmployees", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(totalEmployeesParam);

                await connection.OpenAsync();

                // Execute the query and read the results
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        employees.Add(new EmployeeDto
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                            Name = reader.GetString(reader.GetOrdinal("EmployeeName")),
                            Salary = reader.GetDecimal(reader.GetOrdinal("Salary"))
                        });
                    }

                    // Close the reader before accessing the output parameter
                    await reader.CloseAsync();

                    totalEmployees = (int)totalEmployeesParam.Value;
                }
            }
        }

        return new PaginatedResult<IEnumerable<EmployeeDto>>
        {
            Total = totalEmployees,
            PageSize = filter.PageSize,
            CurrentPage = filter.Page,
            Data = employees
        };
    }

}
