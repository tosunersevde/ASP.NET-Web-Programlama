//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using Entities.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Services.Contracts
{
    public interface IAuthService
    {
        IEnumerable<IdentityRole> Roles { get; } //Sistemdeki rolleri getirir.

        IEnumerable<IdentityUser> GetAllUsers(); //Kullanicilari getirir.

        Task<IdentityUser> GetOneUser(string userName);

        Task<UserDtoForUpdate> GetOneUserForUpdate(string userName); 
        //Normal kullanici bilgisi alinmasina ragmen rollerle birlikte alinamadigi icin bu tanima ihtiyac duyulur.
        Task<IdentityResult> CreateUser(UserDtoForCreation userDto);

        Task Update(UserDtoForUpdate userDto); //geriuye bir sey donmeyeceginden yalin halde

        Task<IdentityResult> ResetPassword(ResetPasswordDto model);
        //True-false seklinde de tanimlanabilirdi ancak error ifadelerini tuttarak genis bilgi araligi saglar.
        Task<IdentityResult> DeleteOneUser(string userName);

    }
}
