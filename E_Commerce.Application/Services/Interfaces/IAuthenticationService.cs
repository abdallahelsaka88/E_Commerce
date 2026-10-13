using E_Commerce.Application.Common;
using E_Commerce.Application.DTOS.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.Interfaces
{
    public interface IAuthenticationServices
    {
        //login
        //Email.password==> (token-Email-DisplayName)

        Task<Result<UserDto>> LoginAsync(LoginDto loginDto, CancellationToken ct = default);

        //Registration
        Task<Result<UserDto>> RegisterAsync(RegisterDto registerDto, CancellationToken ct);

        Task<Result<bool>> CheckEmailExistsAsync(string email, CancellationToken ct = default);

        Task<Result<UserDto>> GetCurrentUserAsync(string email, CancellationToken ct = default);

        Task<Result<AddressDto>> GetCurrentUserAddress(string email, CancellationToken ct);
        Task<Result<AddressDto>> UpdateCurrentUserAddress(string email, AddressDto addressDto, CancellationToken ct);

    }
}
