//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    //Farki uzerinde password bilgisi olmasidir. Diger alanlar kalitimla alinir.
    public record UserDtoForCreation : UserDto 
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        public String? Password { get; init; }
    }
}
