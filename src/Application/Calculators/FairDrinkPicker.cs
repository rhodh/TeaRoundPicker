using Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Application.Calculators
{
    public class FairDrinkPicker : IDrinkPicker
    {
        public User CalculateDrinkUser(IEnumerable<User> users)
        {
            return users.FirstOrDefault();
        }
    }
}
