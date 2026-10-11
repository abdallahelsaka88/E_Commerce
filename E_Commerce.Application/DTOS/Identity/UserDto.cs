using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.DTOS.Identity
{
    public class UserDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string DisplayName { get; set; }
    }
}
