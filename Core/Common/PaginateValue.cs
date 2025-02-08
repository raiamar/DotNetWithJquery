using System.ComponentModel.DataAnnotations;

public abstract class PaginateValue
{
    [Range(1, int.MaxValue, ErrorMessage = "Page must be greater than 0.")]
    public int Page { get; set; } = 1;

    [Range(1, int.MaxValue, ErrorMessage = "Page size must be greater than 0.")]
    public int PageSize { get; set; } = 15;
}
