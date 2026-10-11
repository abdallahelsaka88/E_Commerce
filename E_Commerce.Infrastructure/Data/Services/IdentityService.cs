using E_Commerce.Application.Common;
using E_Commerce.Application.DTOS.Identity;
using E_Commerce.Application.Services.Interfaces;
using E_Commerce.Domain.Common;
using E_Commerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Data.Services
{
    public class IdentityService(UserManager<ApplicationUser> userManager) : IIdentityService
    {
        public async Task<Result<bool>> CheckPasswordAsync(string email, string password, CancellationToken ct)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user is null)
                return Result<bool>.Fail(Error.NotFound("User.NotFound", $"User With Email {email} NotFound"));

            var result = await userManager.CheckPasswordAsync(user, password);
            return result ? Result<bool>.OK(result)
                :
                Result<bool>.Fail(Error.Unauthorized("User.Unauthorized", "User Is Unauthorized"));
        }


        public async Task<Result<IdentityUserResult>> FindUserByEmailAsync(string email, CancellationToken ct)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user is null)
                return Result<IdentityUserResult>.Fail(Error.NotFound("User.NotFound", $"User With Email {email} NotFound!"));

            return Result<IdentityUserResult>.OK(new IdentityUserResult
                (user.Id, user.DisplayName, user.Email, user.UserName));




        }


        public async Task<Result<IdentityUserResult>> CreateUserAsync(RegisterDto registerDto, CancellationToken ct)
        {
            var user = new ApplicationUser()
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber,

            };
            var createResult = await userManager.CreateAsync(user, registerDto.Password);
            if (!createResult.Succeeded)
            {
                var errors = createResult.Errors.Select(e => new Error(e.Code, e.Description)).ToList();
                return Result<IdentityUserResult>.Fail(errors);

            }
            return Result<IdentityUserResult>.OK(new IdentityUserResult(user.Id, user.DisplayName, user.Email, user.UserName));
        }
    }
}
