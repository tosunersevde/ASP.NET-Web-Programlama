using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public record Register
    {
        [Required(ErrorMessage = "Username is required.")]
        public String? UserName { get; init; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public String? Email { get; init; }

        [Required(ErrorMessage = "Password is required.")]
        public String? Password { get; init; }
    }
}
