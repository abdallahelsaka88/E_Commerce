using E_Commerce.Application.DTOS.Identity;
using E_Commerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using StackExchange.Redis.Maintenance;
using System.Formats.Asn1;
using System.Security.Claims;
using System.Threading.Tasks;

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


        //Check Email Exists
        [HttpGet("emailExists/{email}")]
        public async Task<ActionResult<bool>> CheckEmail(string email, CancellationToken ct)
        {
            var result = await authenticationService.CheckEmailExistsAsync(email, ct);
            return ToActionResult(result);
        }


        //Get Current User 

        [HttpGet("CurrentUser")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser(CancellationToken ct)
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new InvalidOperationException();
            var result = await authenticationService.GetCurrentUserAsync(email, ct);
            return ToActionResult(result);
        }


        //Get Current User Address
        [HttpGet("address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress(CancellationToken ct = default)
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new InvalidOperationException();
            var result = await authenticationService.GetCurrentUserAddress(email, ct);
            return ToActionResult(result);
        }
        //Update Current User Address

        [HttpPut("update")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto addressDto, CancellationToken ct = default)
        {
            var email = User.FindFirstValue(ClaimTypes.Email) ?? throw new InvalidOperationException();
            var result = await authenticationService.UpdateCurrentUserAddress(email, addressDto, ct);
            return ToActionResult(result);
        }

    }
}
