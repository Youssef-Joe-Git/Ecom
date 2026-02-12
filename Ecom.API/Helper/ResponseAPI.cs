using System.Collections;

namespace Ecom.API.Helper
{
    public class ResponseAPI
    {
        public ResponseAPI(int statusCode, object data=null, string message = null)
        {
            StatusCode = statusCode;
            Message = message?? GetStatusText(StatusCode);
            if (data != null)
            {
                Data = data;

                if (data is ICollection collection)
                {
                    Length = collection.Count;
                }
                else if (data is IEnumerable<object> enumerable)
                {
                    Length = enumerable.Count();
                }
                else
                {
                    Length = 1; 
                }
            }
        }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public int Length { get; set; } 
        public object? Data { get; set; }

        private string GetStatusText(int statusCode)
        {
            return statusCode switch
            {
                200 => "OK",
                201 => "Created",
                400 => "Bad Request",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "Not Found",
                500 => "Internal Server Error",
                _ => "Unkown Error"
            };
        }

    }
    public class ResponsePagination : ResponseAPI
    {
        public ResponsePagination(int pageNumber, int pageSize, int totalItems,int statusCode, object data = null, string message = null) : base(statusCode, data, message)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItems = totalItems;
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
    }
}
