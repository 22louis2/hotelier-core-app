namespace hotelier_core_app.Model.DTOs.Request
{
    [Serializable]
    public class PageParamsDTO
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

}
