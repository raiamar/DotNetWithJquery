using FluentValidation;
using Microsoft.Data.SqlClient;

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    private readonly string _connectionString;
    public CreateEmployeeCommandValidator(string dbConnection)
    {
        _connectionString = dbConnection;
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Matches(@"^[a-zA-Z\s]+$").WithMessage("Name can only contain letters and spaces.")
            .MustAsync(async (name, cancellationToken) => !await NameExistsInDatabaseAsync(name, cancellationToken))
            .WithMessage("Name already exists.");

        RuleFor(x => x.Salary)
            .GreaterThan(0).WithMessage("Salary must be greater than zero.");
    }



    private async Task<bool> NameExistsInDatabaseAsync(string name, CancellationToken cancellationToken)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync(cancellationToken);

            var query = "SELECT COUNT(1) FROM Employees WHERE Name = @Name";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", name);

                var result = await command.ExecuteScalarAsync(cancellationToken);
                return Convert.ToInt32(result) > 0;
            }
        }
    }
}
