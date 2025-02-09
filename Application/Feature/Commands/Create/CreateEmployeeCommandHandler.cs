using MediatR;

public class CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IDbConnection dbConnection) : IRequestHandler<CreateEmployeeCommand, CommandResponse>
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    private readonly IDbConnection _dbConnection = dbConnection;
    public async Task<CommandResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateEmployeeCommandValidator(_dbConnection.GetConnectionString());
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        var response = new CommandResponse();
        response.Message = "Employee has been created";

        if (validationResult.Errors.Count > 0)
        {
            response.Success = false;
            response.Message = "validation issue";
            response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            return response;
        }
        try
        {
            int id = await _employeeRepository.CreateEmployeeAsync(request.Employee);
            if (id <= 0)
            {
                response.Success = false;
                response.Message = "Something went wrong";
            }
        }catch
        {
            throw;
        }
        return response;
    }
}
