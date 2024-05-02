using BasketballAcademy.DTOs;
using BasketballAcademy.Model;
using System.Security.Claims;

namespace BasketballAcademy.Services.Interfaces
{
    public interface ITokenService
    {
        string? EncryptToCiberText(string combinedId);

        public TokenResponseDto CreateToken(AuthUser user);

        public TokenResponseDto GetToken(IEnumerable<Claim> claim);

    }
}
