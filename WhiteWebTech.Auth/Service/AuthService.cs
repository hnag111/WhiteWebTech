using AutoMapper;
using GreenDonut;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using WhiteWebTech.Auth.Data;
using WhiteWebTech.Auth.Models;
using WhiteWebTech.Auth.Service.IService;

namespace WhiteWebTech.Auth.Service
{
    public class AuthService : IAuthService
    {

        private readonly AppDbContext appDbContext;
        private readonly UserManager<ApplicationUsers> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper _mapper;
        private readonly IJwtTokenGenerator jwtToken;

        public AuthService(AppDbContext appDbContext, IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUsers> userManager, RoleManager<IdentityRole> roleManager, IMapper _mapper)
        {
            this.roleManager = roleManager;
            this.appDbContext = appDbContext;
            this.userManager = userManager;
            this._mapper = _mapper;
            this.jwtToken = jwtTokenGenerator;
        }

        public async Task<bool> AssignRole(string email, string rolename)
        {
            var user = await appDbContext.ApplicationUserss.FirstOrDefaultAsync
                   (e => e.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                if (!roleManager.RoleExistsAsync(rolename).GetAwaiter().GetResult())
                {
                    roleManager.CreateAsync(new IdentityRole(rolename)).GetAwaiter().GetResult();
                }
                await userManager.AddToRoleAsync(user, rolename);
                return true;
            }
            return false;

        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequest)
        {
            try
            {
                LoginResponseDto loginResponseDto = new LoginResponseDto();

                var user = await appDbContext.ApplicationUserss.FirstOrDefaultAsync
                    (e => e.UserName.ToLower() == loginRequest.UserName.ToLower());

                if (user != null)
                {
                    bool IsValid = await userManager.CheckPasswordAsync(user, loginRequest.Password);
                    if (IsValid)
                    {

                        //Token Logic
                        var token = jwtToken.GenerateToken(user);

                        loginResponseDto = new LoginResponseDto()
                        {
                            User = _mapper.Map<UserDto>(user),
                            Token = token
                        };
                    }
                }
                else { return new LoginResponseDto() { User = null }; }

                return loginResponseDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        
        public async Task<object> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUsers user = new ApplicationUsers()
            {
                Name = registrationRequestDto.Name,
                Email = registrationRequestDto.Email,
                PhoneNumber = registrationRequestDto.PhoneNumber,
                UserName = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                Address = registrationRequestDto.Address
            };

            try
            {
                var result = await userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    var retrunUser = await appDbContext.ApplicationUserss.FirstOrDefaultAsync(e => e.UserName == registrationRequestDto.Email);
                    UserDto userDto = new UserDto();
                    userDto = _mapper.Map<UserDto>(retrunUser);
                    return userDto;
                }
                else
                {
                    ErrorMessges errorMessges = new ErrorMessges()
                    {
                        errormessge = result.Errors.FirstOrDefault().Description
                    };
                    return errorMessges;
                }

            }
            catch (Exception ex)
            {
                ErrorMessges error = new ErrorMessges()
                {
                    errormessge = ex.Message
                };
                return error;
            }
        }
    }
}
