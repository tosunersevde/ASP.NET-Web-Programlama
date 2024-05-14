//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Entities.Dtos
{
    //Sadece veriler tasinacagindan, gelen veriler uzerinde degistirme yapilmayacagindan dto tanimi kullanildi.
    public record RegisterDto //record ifadesinde degerler tanimlandigi anda verilmeli, set yerine init olur.
    {
        [Required(ErrorMessage ="Username is required.")]
        public String? UserName { get; init; }

        [Required(ErrorMessage = "Email is required.")]
        public String? Email { get; init; }

        [Required(ErrorMessage = "Password is required.")]
        public String? Password { get; init; }

        //Ad-soyad gibi alanlar olabilirdi ancak IdentityUser genisletilmedi.
    }
}
