using AutoMapper;
using Domain.Dto;
using Domain.Models;
using System;

namespace Domain.Mappers
{
    public class UserDomainProfile : Profile
    {
        public UserDomainProfile()
        {
            CreateMap<UserDto, User>()
                .ConvertUsing(x => new User(Guid.NewGuid(), x.FirstName, x.LastName));
        }
    }
}
