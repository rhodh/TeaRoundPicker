using Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Application.Calculators
{
    public class FairDrinkPicker : IDrinkPicker
    {
        public User CalculateDrinkUser(IEnumerable<User> users)
        {
            return users.Aggregate((lhs, rhs) => GetUserOrderComplicationFactor(lhs) >= GetUserOrderComplicationFactor(rhs) ? lhs : rhs );
        }

        private static int GetUserOrderComplicationFactor(User user)
        {
            return user.DrinkOrders.FirstOrDefault()?.AdditionalSpecification?.Count ?? 0;
        }
    }
}
