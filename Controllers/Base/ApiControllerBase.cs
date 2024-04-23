using BasketballAcademy.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BasketballAcademy.Controllers.Base
{
    public class ApiControllerBase : ControllerBase
    {
        protected IActionResult ApiResponse<T>(ApiResponse<T> response)
        {
            return StatusCode(response.StatusCode, response);
        }

        protected IActionResult ApiOkResponse<T>(T data)
        {
            return ApiResponse(new ApiResponse<T>((int)HttpStatusCode.OK, "Success", data, null));
        }

    }
}
