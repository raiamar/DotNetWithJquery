using MediatR;

public class CreateEmployeeCommand : IRequest<CommandResponse>
{
    public string Name { get; set; } = default!;
    public decimal Salary { get; set; }
}
