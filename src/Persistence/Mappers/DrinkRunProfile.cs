using AutoMapper;
using Domain.Models;
using Persistence.DBModels;

namespace Persistence.Mappers
{
    public class DrinkRunProfile : Profile
    {
        public DrinkRunProfile()
        {
            CreateMap<DrinkRun, DrinkRunDbModel>()
                .ForMember(x => x.DrinkMakerId, opt => opt.MapFrom(x => x.DrinkMaker.Id))
                .ForMember(x => x.DrinkMaker, opt => opt.Ignore());

            CreateMap<DrinkRunDbModel, DrinkRun>()
                .ConstructUsing((x, context) =>  
                    new DrinkRun(x.DrinkMakerId, context.Mapper.Map<User>(x.DrinkMaker)));
        }
    }
}
