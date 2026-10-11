using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Common
{
    public record class Error(string Code, string Description, ErrorType ErrorType = ErrorType.InvalidCredentials)
    {
        public static Error Failure(string code = "General.Failure", string description = "General Failure Has Occured")
            => new Error(code, description, ErrorType.Failure);
        public static Error Validation(string code = "General.Validation", string description = "General Validation Has Occured")
            => new Error(code, description, ErrorType.Validation);
        public static Error NotFound(string code = "General.NotFound", string description = "General NotFound Has Occured")
            => new Error(code, description, ErrorType.NotFound);
        public static Error Conflict(string code = "General.Conflict", string description = "General Conflict Has Occured")
            => new Error(code, description, ErrorType.Conflict);
        public static Error Unauthorized(string code = "General.Unauthorized", string description = "General Unauthorized Has Occured")
            => new Error(code, description, ErrorType.Unauthorized);
        public static Error Forbidden(string code = "General.Forbidden", string description = "General Forbidden Has Occured")
            => new Error(code, description, ErrorType.Forbidden);
        public static Error InvalidCredentials(string code = "General.InvalidCredentials", string description = "General InvalidCredentials Has Occured")
            => new Error(code, description, ErrorType.InvalidCredentials);
    }

    public enum ErrorType
    {
        Failure,
        Validation,
        NotFound,
        Conflict,
        Unauthorized,
        Forbidden,
        InvalidCredentials,
    }
}
