using AutoMapper;
using DataAccess.Models;
using Home_API.Models.Dtos;

namespace Home_API;

public class MapperConfig: Profile
{
    public MapperConfig()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}