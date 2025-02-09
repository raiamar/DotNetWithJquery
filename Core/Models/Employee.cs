using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
public class Employee
{
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; } = default!;
    public decimal Salary { get; set; }
}
