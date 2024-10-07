using WhiteWebTech.Auth.Models;

namespace WhiteWebTech.Auth.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUsers applicationUsers);
    }
}
