//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace Entities.Dtos
{
    public record UserDtoForUpdate : UserDto
    {
        //Set -> tekrar eden bir string ifadesi buraya eklenemez.
        //Referans tipli, tanimlandiktan sonra baslatilmali, referans almali, new'lenmeli.
        public HashSet<string> UserRoles { get; set; } = new HashSet<string>();
    }
}
