using ApiGateway.Modals;
using System.Security.Claims;
using GATEWAY_SERVICE.Modals;

namespace GATEWAY_SERVICE.Services
{
    public interface ITokenService
    {
        public string GenerateToken(UserViewModel request);
        string GenerateRefreshToken();
    }
}
