using FluentValidation;
using MediatR;

public class UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IDbConnection dbConnection) : IRequestHandler<UpdateEmployeeCommand, CommandResponse>
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    private readonly IDbConnection _connection = dbConnection;
    public async Task<CommandResponse> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateEmployeeCommandValidator(_connection.GetConnectionString());
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        var response = new CommandResponse();
        response.Message = "Employee salary updated";

        if (validationResult.Errors.Count > 0)
        {
            response.Success = false;
            response.Message = "validation issue";
            response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return response;
        }

        try
        {
            bool result = await _employeeRepository.UpdateEmployeeSalaryAsync(request.Employee);
            if (!result)
            {
                response.Success = false;
                response.Message = "Something went wrong";
            }
        }
        catch
        {
            throw;
        }
        return response;
    }
}
