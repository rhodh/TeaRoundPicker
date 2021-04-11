using AutoMapper;
using Domain.Dto;
using Domain.Models;
using System;

namespace Domain.Mappers
{
    public class DrinkOrderDomainProfile : Profile
    {
        public DrinkOrderDomainProfile()
        {
            CreateMap<DrinkOrderDto, DrinkOrder>()
                .ConvertUsing(x => new DrinkOrder(Guid.NewGuid(), x.Name, x.Type, x.AdditionalSpecification));
        }
    }
}
