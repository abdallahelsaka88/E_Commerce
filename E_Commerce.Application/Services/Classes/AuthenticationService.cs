using E_Commerce.Application.Common;
using E_Commerce.Application.DTOS.Identity;
using E_Commerce.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.Classes
{
    public class AuthenticationService(IIdentityService identityService) : IAuthenticationServices
    {
        public async Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default)
        {
            //check email
            var userResult = await identityService.FindUserByEmailAsync(loginDto.Email, ct);
            if (!userResult.IsSuccess)
                return Result<UserDto>.Fail(userResult.Errors);



            //check password
            var passswordResult = await identityService.CheckPasswordAsync(loginDto.Email, loginDto.Password, ct);
            if (!passswordResult.IsSuccess)
                return Result<UserDto>.Fail(passswordResult.Errors);




            //return userDto
            var user = userResult.Data;
            return Result<UserDto>.OK(new UserDto()
            {
                Email = user.Email,
                Token = "TODO",
                DisplayName = user.DisplayName,
            });


        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken ct)
        {
            //Create User
            var createResult = await identityService.CreateUserAsync(registerDto, ct);


            if (!createResult.IsSuccess)
                return Result<UserDto>.Fail(createResult.Errors);
            var user = createResult.Data;

            return Result<UserDto>.OK(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = "TODO"

            });

        }
    }
}
