using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhiteWebTech.Auth.Models;
using WhiteWebTech.Auth.Service.IService;

namespace WhiteWebTech.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly IAuthService authService;
        protected ResponseDTO responseDTO;
        public AuthApiController(IAuthService authService)
        {
            responseDTO = new ResponseDTO();
            this.authService = authService;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationRequest)
        {
            try
            {
                var data = await authService.Register(registrationRequest);
                responseDTO.Result = data;

                return Ok(responseDTO);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            try
            {
                var loginResponse = await authService.Login(loginRequest);
                if (loginResponse.User == null)
                {
                    responseDTO.Result = null;
                    responseDTO.Message = "UserName Or Password is Incorrect";
                    responseDTO.IsSuccess = false;
                    return BadRequest(responseDTO);
                }

                responseDTO.Result = loginResponse;
                responseDTO.IsSuccess=true;

                return Ok(responseDTO);
            }
            catch (Exception)
            {

                throw;
            }

        }


        [HttpGet]
        [Route("AssignRole")]
        public async Task<IActionResult> AssignRole(string Email, string Rolename)
        {
            try
            {
                var loginResponse = await authService.AssignRole(Email, Rolename);
                if (!loginResponse )
                {
                    responseDTO.Result = null;
                    responseDTO.Message = "Error encounter";
                    responseDTO.IsSuccess = false;
                    return BadRequest(responseDTO);
                }

                responseDTO.Result = loginResponse;

                return Ok(responseDTO);
            }
            catch (Exception)
            {

                throw;
            }

        }





    }
}
