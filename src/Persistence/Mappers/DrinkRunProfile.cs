using AutoMapper;
using Domain.Models;
using Persistence.DBModels;
using System.Collections.Generic;

namespace Persistence.Mappers
{
    public class DrinkRunProfile : Profile
    {
        public DrinkRunProfile()
        {
            CreateMap<DrinkRun, DrinkRunDbModel>()
                .ForMember(x => x.DrinkMakerId, opt => opt.MapFrom(x => x.DrinkMaker.Id))
                .ForMember(x => x.DrinkMaker, opt => opt.Ignore())
                .ForMember(x => x.DrinkOrders, opt => opt.Ignore());

            CreateMap<DrinkRunDbModel, DrinkRun>()
                .ConstructUsing((x, context) =>
                    new DrinkRun(x.DrinkMakerId,
                    context.Mapper.Map<User>(x.DrinkMaker),
                    context.Mapper.Map<IEnumerable<DrinkOrder>>(x.DrinkOrders)))
                .ForMember(x => x.Orders, opt => opt.Ignore());
               
        }
    }
}
