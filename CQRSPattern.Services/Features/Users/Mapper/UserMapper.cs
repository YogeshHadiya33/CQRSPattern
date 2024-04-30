using AutoMapper;
using CQRSPattern.Entity.User.Bussiness;
using CQRSPattern.Entity.User.Database;

namespace CQRSPattern.Services.Features.Users.Mapper;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, UserModel>();
    }
}