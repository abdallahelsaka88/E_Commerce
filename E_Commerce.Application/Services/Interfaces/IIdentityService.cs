using E_Commerce.Application.Common;
using E_Commerce.Application.DTOS.Identity;
using E_Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Result<IdentityUserResult>> FindUserByEmailAsync(string email, CancellationToken ct);
        Task<Result<bool>> CheckPasswordAsync(string email, string password, CancellationToken ct);
        Task<Result<IdentityUserResult>> CreateUserAsync(RegisterDto registerDto, CancellationToken ct);
        Task<Result<IReadOnlyList<string>>> GetUserRoleAsync(string email, CancellationToken ct);
        Task<Result<AddressDto>> GetCurrentUserAddressAsync(string email, CancellationToken ct);
        Task<Result<AddressDto>> UpdateGetCurrentUserAddressAsync(string email, AddressDto addressDto, CancellationToken ct);


    }
}
