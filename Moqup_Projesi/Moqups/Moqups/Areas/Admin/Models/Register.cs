using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Moqups.Areas.Admin.Models
{
    public record Register
    {
        [Required(ErrorMessage = "UserName is required.")]
        public String? UserName { get; init; }

        [Required(ErrorMessage = "Email is required.")]
        public String? Email { get; init; }

        [Required(ErrorMessage = "Password is required.")]
        public String? Password { get; init; }
    }
}
