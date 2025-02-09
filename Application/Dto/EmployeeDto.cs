public class EmployeeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public decimal Salary { get; set; }
}

public class CreateEmployeeDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Salary { get; set; }
}


public class UpdateEmployeeDto
{
    public int Id { get; set; }
    public decimal Salary { get; set; }
}