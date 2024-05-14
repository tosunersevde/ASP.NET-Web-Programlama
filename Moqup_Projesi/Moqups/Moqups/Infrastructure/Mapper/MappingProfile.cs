using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace Moqups.Infrastructure.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDtoForCreation, IdentityUser>();
            CreateMap<UserDtoForUpdate, IdentityUser>().ReverseMap(); //Dto'dan IdentityUser'a donus yapilabilir ve identityuser'dan dto'ya reverseMap ile donus yapilabilir.
        }

    }
}
