using Application.HttpExceptions;
using AutoMapper;
using Domain.Dto;
using Domain.Models;
using Persistence.UserRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserWriter _userWriter;
        private readonly IUserReader _userReader;
        private readonly IMapper _mapper;

        public UserService(IUserWriter userWriter, IUserReader userReader, IMapper mapper)
        {
            _userWriter = userWriter ?? throw new ArgumentNullException(nameof(userWriter));
            _userReader = userReader ?? throw new ArgumentNullException(nameof(userReader));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<User> CreateUser(UserDto user)
        {
            User newUser = _mapper.Map<User>(user);
            return await _userWriter.CreateUser(newUser);
        }

        public async Task<User> GetUser(Guid userId)
        {
            return await _userReader.GetUser(userId) ?? throw new UserNotFoundException(userId);
        }

        public async Task<DrinkOrder> AddDrinkOrder(Guid id, DrinkOrderDto drinkOrderDto)
        {
            User user = await GetUser(id);

            if(user.DrinkOrders.Any())
            {
                throw new OverOrderLimitException(user);
            }

            //Attach user
            drinkOrderDto.UserId = id;

            return await _userWriter.CreateDrinkOrder(_mapper.Map<DrinkOrder>(drinkOrderDto));
        }
    }
}
