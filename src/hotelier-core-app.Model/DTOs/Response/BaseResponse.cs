namespace hotelier_core_app.Model.DTOs.Response
{
    public class BaseResponse
    {
        public bool Status { get; set; }
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public static BaseResponse Success(string message = "", string statusCode = "")
        {
            return new BaseResponse()
            {
                Status = true,
                StatusCode = statusCode ?? "00",
                Message = message ?? "Operation successful!"
            };
        }

        public static BaseResponse Failure(string message = "", string statusCode = "")
        {
            return new BaseResponse()
            {
                StatusCode = statusCode ?? "06",
                Message = message ?? "Sorry, there was an error processing your request."
            };
        }
    }

    public class BaseResponse<T> : BaseResponse
    {
        public T Data { get; set; }
        public static BaseResponse<T> Success(T data, string message = "", string statusCode = "")
        {
            return new BaseResponse<T>()
            {
                Status = true,
                StatusCode = statusCode ?? "00",
                Message = message ?? "Operation successful!",
                Data = data
            };
        }

        public static BaseResponse<T> Failure(T data, string message = "", string statusCode = "")
        {
            return new BaseResponse<T>()
            {
                StatusCode = statusCode ?? "06",
                Message = message ?? "Sorry, there was an error processing your request.",
                Data = data
            };
        }
    }

    public class PageBaseResponse<T> : BaseResponse
    {
        public T Data { get; set; }
        public int? DataCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPageCount { get; set; }
        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }
        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPageCount);
            }
        }

        public static PageBaseResponse<T> Success(T data, string message = "", int? count = 1, string statusCode = "")
        {
            return new PageBaseResponse<T>()
            {
                Status = true,
                StatusCode = statusCode ?? "00",
                Message = message ?? "Operation successful!",
                Data = data,
                DataCount = count
            };
        }

        public static PageBaseResponse<T> Failure(T data, string message = "", int? count = 0, string statusCode = "")
        {
            return new PageBaseResponse<T>()
            {
                StatusCode = statusCode ?? "06",
                Message = message ?? "Sorry, there was an error processing your request.",
                Data = data,
                DataCount = count
            };
        }

        public static PageBaseResponse<T> Success(T data, string message = "", int count = 1, string statusCode = "", int totalPageCount = 1, int pageNumber = 1, int pageSize = 1)
        {
            return new PageBaseResponse<T>()
            {
                Status = true,
                StatusCode = statusCode ?? "00",
                Message = message ?? "Operation successful!",
                Data = data,
                DataCount = count,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPageCount = totalPageCount
            };
        }
    }
}
