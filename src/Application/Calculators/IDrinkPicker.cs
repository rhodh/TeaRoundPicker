using Domain.Models;
using System.Collections.Generic;

namespace Application.Calculators
{
    public interface IDrinkPicker
    {
        User CalculateDrinkUser(IEnumerable<User> users);
    }
}
