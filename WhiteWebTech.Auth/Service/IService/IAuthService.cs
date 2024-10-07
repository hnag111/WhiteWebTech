using WhiteWebTech.Auth.Models;

namespace WhiteWebTech.Auth.Service.IService
{
    public interface IAuthService
    {
        Task<object> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequest);

        Task<bool> AssignRole(string email, string rolename);

    }
}
