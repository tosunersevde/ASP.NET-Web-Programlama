using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Moqups.Areas.Admin.Models
{
    public record User
    {
        [DataType(DataType.Text)] 
        [Required(ErrorMessage = "UserName is required.")]
        public String? UserName { get; init; } 

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required.")]
        public String? Email { get; init; }

        //[Required(ErrorMessage = "Password is required.")]
        //public string? Password { get; set; }

        public HashSet<String> Roles { get; set; } = new HashSet<string>();
    }
}
