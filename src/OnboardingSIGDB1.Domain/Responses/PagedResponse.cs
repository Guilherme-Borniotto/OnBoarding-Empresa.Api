namespace OnboardingSIGDB1.Domain.Responses;

public class PagedResponse<T> where T : class
{
    
    public IEnumerable<T> Data { get; set; }
        
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
    
    public PagedResponse(IEnumerable<T> data, int pageNumber, int pageSize, int totalRecords)
    {
        Data = data;
        PageNumber = pageNumber;
        PageSize = pageSize;
        Total = totalRecords;
    }
}
