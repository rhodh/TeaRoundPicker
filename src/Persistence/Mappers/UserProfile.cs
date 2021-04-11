using AutoMapper;
using Domain.Models;
using Persistence.DBModels;

namespace Persistence.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDbModel>();

            CreateMap<UserDbModel, User>()
                .ConvertUsing(x => new User(x.Id, x.FirstName, x.LastName));
        }
    }
}
