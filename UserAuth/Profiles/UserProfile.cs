using AutoMapper;
using UserAuth.Data.Dtos;
using UserAuth.Model;

namespace UserAuth.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>();
        }
    }
}
