using Domain.Dto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IUserService
    {
        Task<User> GetUser(Guid userId);

        Task<User> CreateUser(UserDto user);
        Task<DrinkOrder> AddDrinkOrder(Guid id, DrinkOrderDto drinkOrderDto);
    }
}
