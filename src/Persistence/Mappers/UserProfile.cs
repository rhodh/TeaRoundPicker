using AutoMapper;
using Domain.Models;
using Persistence.DBModels;
using System;
using System.Collections.Generic;

namespace Persistence.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDbModel>();

            CreateMap<UserDbModel, User>()
                .ConstructUsing((x, context) =>  
                    new User(x.Id, 
                        x.FirstName, 
                        x.LastName, 
                        context.Mapper.Map<IEnumerable<DrinkOrder>>(x.DrinkOrders)));
        }
    }
}
