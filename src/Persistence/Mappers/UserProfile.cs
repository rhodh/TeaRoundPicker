using AutoMapper;
using Domain.Models;
using Persistence.DBModels;

namespace Persistence.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDbModel>()
                .ForMember(x => x.DrinkOrders, opt => opt.Ignore());

            CreateMap<UserDbModel, User>()
                .ConvertUsing(x => new User(x.Id, x.FirstName, x.LastName));
        }
    }
}
