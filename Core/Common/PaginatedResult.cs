using System.Collections;
using System.Linq;

public class PaginatedResult<T>
{
    public int Total { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public T? Data { get; set; }
    public int TotalPages
    {
        get
        {
            if (PageSize <= 0) { return 0; }
            return (int)Math.Ceiling((double)Total / PageSize);
        }
    }

    public int ItemsInCurrentPage
    {
        get
        {
            if (Data is ICollection collection)
            {
                return collection.Count;
            }

            if (Data is IEnumerable enumerable)
            {
                int count = 0;
                foreach (var _ in enumerable)
                {
                    count++;
                }
                return count;
            }

            return 0;
        }
    }
}
