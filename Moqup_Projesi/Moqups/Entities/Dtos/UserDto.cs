//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    public record UserDto
    {
        [DataType(DataType.Text)] //Form elemanlarini bicimlendirir.
        [Required(ErrorMessage ="Username is required.")]
        public String? UserName { get; init; } //init ile initialize samasinda degerler verilir, sonra degistirilmez.

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required.")]
        public String? Email { get; init; }

        [DataType(DataType.PhoneNumber)]
        public String? PhoneNumber { get; init; }

        public HashSet<String> Roles { get; set; } = new HashSet<string>(); //Tanimlandigi yerde dogrudan baslatilir.
        //Roller tekrar edemeyecek.

        //Sifre alani insertion'a birakilacak - normal sartlarda sifre kullanilmayacak
    }
}
