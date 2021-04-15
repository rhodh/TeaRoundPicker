using Application.Calculators;
using Application.HttpExceptions;
using Domain.Dto;
using Domain.Models;
using Persistence.DrinkRunRepo;
using Persistence.UserRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DrinkRunService : IDrinkRunService
    {
        private readonly IUserReader _userReader;
        private readonly IDrinkPicker _drinkPicker;
        private readonly IDrinkRunWriter _drinkRunWriter;

        public DrinkRunService(IUserReader userReader, IDrinkPicker drinkPicker, IDrinkRunWriter drinkRunWriter)
        {
            _userReader = userReader ?? throw new ArgumentNullException(nameof(userReader));
            _drinkPicker = drinkPicker ?? throw new ArgumentNullException(nameof(drinkPicker));
            _drinkRunWriter = drinkRunWriter ?? throw new ArgumentNullException(nameof(drinkRunWriter));
        }

        public async Task<DrinkRun> CreateDrinkRun(DrinkRunDto drinkRunDto)
        {
            var users = await _userReader.GetUsers(drinkRunDto.Particpants.Select(x => x.UserId));

            ValidateUsers(users);

            User drinkMaker = _drinkPicker.CalculateDrinkUser(users);
            return await _drinkRunWriter.CreateDrinkRun(new DrinkRun(Guid.NewGuid(), drinkMaker));
        }

        private static void ValidateUsers(IEnumerable<User> users)
        {
            var missingOrders = users.Where(x => !x.DrinkOrders.Any());
            if (missingOrders.Any())
            {
                throw new OrderNotDefinedException(missingOrders.Select(x => x.Id));
            }
        }
    }
}
