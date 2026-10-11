using E_Commerce.Application.DTOS.Identity;
using E_Commerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;

namespace GymManagementApp.API.Controllers
{
    public class AuthenticationController(IAuthenticationServices authenticationService) : ApiBaseController
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto, CancellationToken ct)
        {
            var result = await authenticationService.LoginAsync(loginDto, ct);
            return ToActionResult(result);
        }


        //register
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto, CancellationToken ct)
        {
            var register = await authenticationService.RegisterAsync(registerDto, ct);
            return ToActionResult(register);
        }

    }
}
