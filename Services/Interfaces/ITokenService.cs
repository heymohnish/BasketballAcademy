using BasketballAcademy.DTOs;
using BasketballAcademy.Model;
using System.Security.Claims;

namespace BasketballAcademy.Services.Interfaces
{
    public interface ITokenService
    {

        public string CreateToken();

    }
}
