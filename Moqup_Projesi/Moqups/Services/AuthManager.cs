//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using AutoMapper;
using Entities.Dtos;
using Microsoft.AspNetCore.Identity;

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using Services.Contracts;

namespace Services
{
    public class AuthManager : IAuthService
    {
        //AuthManager new'lendiginde hangi nesnelere ihtiyac var
        //-> Rollerin listesi verilecek - RoleManager'a ihtiyac olur.
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly IMapper _mapper;
        public AuthManager(RoleManager<IdentityRole> roleManager, 
            UserManager<IdentityUser> userManager,
            IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public IEnumerable<IdentityRole> Roles => 
            _roleManager.Roles;

        public async Task<IdentityResult> CreateUser(UserDtoForCreation userDto)
        {
            //Implementasyonda ilk ihtiyac duyulan userDto'daki alanlari IdentityUser'a map'lemektir.
            var user = _mapper.Map<IdentityUser>(userDto);
            var result = await _userManager.CreateAsync(user, userDto.Password); //Kullanici olusur.

            if (!result.Succeeded)
                throw new Exception("User could not be created.");

            if (userDto.Roles.Count > 0)
            {
                var roleResult = await _userManager.AddToRolesAsync(user, userDto.Roles);
                if(!roleResult.Succeeded) //Role eklemede sikinti varsa
                    throw new Exception("System have a problem with roles.");
            }

            return result;
        }

        public async Task<IdentityResult> DeleteOneUser(string userName)
        {
            var user = await GetOneUser(userName); //Bir kullanici getirilir.
            return await _userManager.DeleteAsync(user); //Identity result
        }

        public IEnumerable<IdentityUser> GetAllUsers()
        {
            return _userManager.Users.ToList();
        }

        public async Task<IdentityUser> GetOneUser(string userName)
        {
            //return await _userManager.FindByNameAsync(userName); //Tek bir kullaniciyi getirir.
            //Kullanici varsa donecek sekilde yoksa hata firlatacak sekilde kurgulanmasi daha dogrudur.
            var user = await _userManager.FindByNameAsync(userName);
            if(user is not null)
                return user;
            throw new Exception("User could not be found!");
        }

        public async Task<UserDtoForUpdate> GetOneUserForUpdate(string userName)
        {
            var user = await GetOneUser(userName);
            var userDto = _mapper.Map<UserDtoForUpdate>(user);
            userDto.Roles = new HashSet<string>(Roles.Select(r => r.Name).ToList()); //Butun roller
            userDto.UserRoles = new HashSet<string>(await _userManager.GetRolesAsync(user));
            return userDto;

            //var user = await GetOneUser(userName);
            //if(user is not null) //Boyle bir kullanici var ise
            //{
            //    var userDto = _mapper.Map<UserDtoForUpdate>(user);
            //    userDto.Roles = new HashSet<string>(Roles.Select(r => r.Name).ToList()); //Butun roller
            //    userDto.UserRoles = new HashSet<string>(await _userManager.GetRolesAsync(user));
            //    return userDto;
            //}
            //throw new Exception("An error occured!");
        }

        public async Task<IdentityResult> ResetPassword(ResetPasswordDto model)
        {
            var user = await GetOneUser(model.UserName); //Kullaniciyi getitir.
            await _userManager.RemovePasswordAsync(user); //Kullanici sifresi kaldirilir.
            var result = await _userManager.AddPasswordAsync(user, model.Password); //Sifre eklenir.
            return result;

            //var user = await GetOneUser(model.UserName); //Kullaniciyi getitir.
            //if( user is not null ) //Kullanici varsa
            //{
            //    await  _userManager.RemovePasswordAsync(user); //Kullanici sifresi kaldirilir.
            //    var result = await _userManager.AddPasswordAsync(user, model.Password); //Sifre eklenir.
            //    return result;
            //}
            //throw new Exception("User could not be found!");
        }

        public async Task Update(UserDtoForUpdate userDto)
        {
            var user = await GetOneUser(userDto.UserName);
            user.PhoneNumber = userDto.PhoneNumber;
            user.Email = userDto.Email;
            var result = await _userManager.UpdateAsync(user); //Kullaniciyi guncelle

            if (userDto.Roles.Count > 0) //Rolleri guncelle
            {
                var userRoles = await _userManager.GetRolesAsync(user); //Kullanici rolleri alindi.
                var r1 = await _userManager.RemoveFromRolesAsync(user, userRoles); //Kullanici uzerindeki roller kaldirildi.
                var r2 = await _userManager.AddToRolesAsync(user, userDto.Roles); //Roller kullaniciya atandi.
            }
            return;
            

            //var user = await GetOneUser(userDto.UserName);
            //user.PhoneNumber = userDto.PhoneNumber;
            //user.Email = userDto.Email;

            //if(user is not null) //Boyle bir kullanici varsa
            //{
            //    var result = await _userManager.UpdateAsync(user); //Kullaniciyi guncelle

            //    if (userDto.Roles.Count > 0) //Rolleri guncelle
            //    {
            //        var userRoles = await _userManager.GetRolesAsync(user); //Kullanici rolleri alindi.
            //        var r1 = await _userManager.RemoveFromRolesAsync(user, userRoles); //Kullanici uzerindeki roller kaldirildi.
            //        var r2 = await _userManager.AddToRolesAsync(user, userDto.Roles); //Roller kullaniciya atandi.
            //    }
            //    return;
            //}
            //throw new Exception("System has a problem with user update!");
        }
    }
}
