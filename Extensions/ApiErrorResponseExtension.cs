using System.Net;

namespace BasketballAcademy.Extensions
{
    //public static class ApiErrorResponseExtension
    //{
    //    public static ApiErrorResponse CreateErrorResponse(this Exception ex)
    //    {
    //        int statusCode = (int)HttpStatusCode.BadRequest;
    //        string message = Convert.ToString(HttpStatusCode.BadRequest);
    //        string error = ex.Message;

    //        if (ex is GenieException genieException)
    //        {
    //            statusCode = genieException.StatusCode;
    //            error = genieException.ErrorCode;
    //        }
    //        else if (ex is AuthorizationException)
    //        {
    //            statusCode = (int)HttpStatusCode.Unauthorized;
    //        }

    //        return new ApiErrorResponse(statusCode, message, null, error);
    //    }
    //}


    public class ApiErrorResponse : ApiResponse<object>
    {
        public ApiErrorResponse(int statusCode, string message, object data, string error)
            : base(statusCode, message, data, error)
        {
        }
    }


    public class ApiResponse
    {
        public int StatusCode { get; }

        public ApiResponse(int statusCode)
        {
            StatusCode = statusCode;
        }

    }
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

    public class ApiInsertResponse : ApiResponse
    {
        public ApiInsertResponse(int id) : base((int)HttpStatusCode.OK)
        {
            Id = id;

        }
        public int Id { get; }
    }
}
