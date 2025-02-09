namespace hotelier_core_app.Model.DTOs.Request;

public class PaginationInputDTO
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}