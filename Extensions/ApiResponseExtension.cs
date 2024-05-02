using System.Net;

namespace BasketballAcademy.Extensions
{
    
    public class ApiResponse<T>
    {
        public int StatusCode { get; }
        public string Message { get; }
        public T Data { get; }
        public string Error { get; }

        public ApiResponse(int statusCode, string message, T data, string error)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
            Error = error;
        }
    }
}
