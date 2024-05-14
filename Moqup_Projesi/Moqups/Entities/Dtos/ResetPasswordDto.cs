//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public record ResetPasswordDto
    {
        public String? UserName { get; init; }

        [Required(ErrorMessage ="Password is required.")]
        [DataType(DataType.Password)]
        public String? Password { get; init; }

        [Required(ErrorMessage = "ConfirmPassword is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password and ConfirmPassword must be match!")]
        //Pasword eslesme kontrolu
        public String? ConfirmPassword { get; init; }
    }
}
