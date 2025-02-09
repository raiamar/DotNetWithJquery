using MediatR;
using System.ComponentModel.DataAnnotations;

public class UpdateEmployeeCommand : IRequest<CommandResponse>
{
    [Required]
    public int Id { get; set; }
    public decimal Salary { get; set; }
}
