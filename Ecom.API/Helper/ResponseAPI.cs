namespace Ecom.API.Helper
{
    public class ResponseAPI
    {
        public ResponseAPI(int statusCode, object data=null, string message = null)
        {
            StatusCode = statusCode;
            Message = message?? GetStatusText(StatusCode);
            Data = data;
        }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
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
    public class ApiExceptions : ResponseAPI
    {

        public ApiExceptions(int statusCode, string message,string data=null) : base(statusCode, data, message)
        {
        }
    }
}
