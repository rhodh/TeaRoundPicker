using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class User
    {
        public User(Guid id, string firstName, string lastName, IEnumerable<DrinkOrder> drinkOrders)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DrinkOrders = drinkOrders;
        }

        public Guid Id { get; }
        public string LastName { get; }
        public IEnumerable<DrinkOrder> DrinkOrders { get; }
        public string FirstName { get; }
    }
}
