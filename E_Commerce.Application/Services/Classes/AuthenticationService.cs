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
    public class AuthenticationService(IIdentityService identityService, ITokenService tokenService) : IAuthenticationServices
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

            var rolesResult = await identityService.GetUserRoleAsync(userResult.Data.Email, ct);
            var token = await tokenService.CreateTokenAsync(userResult.Data.Id, userResult.Data.Email, userResult.Data.UserName, rolesResult.Data);

            //return userDto
            var user = userResult.Data;
            return Result<UserDto>.OK(new UserDto()
            {
                Email = user.Email,
                Token = token,
                DisplayName = user.DisplayName,
            });


        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken ct)
        {
            //Create User
            var userResult = await identityService.CreateUserAsync(registerDto, ct);


            if (!userResult.IsSuccess)
                return Result<UserDto>.Fail(userResult.Errors);
            var user = userResult.Data;

            var rolesResult = await identityService.GetUserRoleAsync(userResult.Data.Email, ct);
            var token = await tokenService.CreateTokenAsync(userResult.Data.Id, userResult.Data.Email, userResult.Data.UserName, rolesResult.Data);


            return Result<UserDto>.OK(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = token

            });

        }
    }
}
