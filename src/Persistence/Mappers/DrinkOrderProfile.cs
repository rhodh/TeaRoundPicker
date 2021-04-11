using AutoMapper;
using Domain.Models;
using Persistence.DBModels;

namespace Persistence.Mappers
{
    public class DrinkOrderProfile : Profile
    {
        public DrinkOrderProfile()
        {
            CreateMap<DrinkOrder, DrinkOrderDbModel>()
                .ForMember(x => x.User, opt => opt.Ignore());

            CreateMap<DrinkOrderDbModel, DrinkOrder>()
                .ConvertUsing(x => new DrinkOrder(x.Id, x.Name, x.Type, x.AdditionalSpecification));
        }
    }
}
