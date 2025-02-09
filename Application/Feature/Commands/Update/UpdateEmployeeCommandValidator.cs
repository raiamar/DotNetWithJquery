using FluentValidation;
using Microsoft.Data.SqlClient;
public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    private readonly string _connectionString;

    public UpdateEmployeeCommandValidator(string connectionString)
    {
        _connectionString = connectionString;

        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Employee ID is required.")
            .MustAsync(async (id, cancellationToken) => await EmployeeExistsInDatabaseAsync(id, cancellationToken))
            .WithMessage("Invalid Employee ID. Employee does not exist.");

        RuleFor(x => x.Salary)
            .GreaterThan(0).WithMessage("Salary must be greater than zero.");
    }

    private async Task<bool> EmployeeExistsInDatabaseAsync(int employeeId, CancellationToken cancellationToken)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync(cancellationToken);

            var query = "SELECT COUNT(1) FROM SW_TBL_EMPLOYEE WHERE Id = @Id";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", employeeId);

                var result = await command.ExecuteScalarAsync(cancellationToken);
                return Convert.ToInt32(result) > 0;
            }
        }
    }
}
