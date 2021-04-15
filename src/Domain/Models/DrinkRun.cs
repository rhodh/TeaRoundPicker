using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class DrinkRun
    {
        public DrinkRun(Guid id, User drinkMaker, IEnumerable<DrinkOrder> orders)
        {
            Id = id;
            DrinkMaker = drinkMaker;
            Orders = orders;
        }

        public Guid Id { get;  }
        public User DrinkMaker { get; }
        public IEnumerable<DrinkOrder> Orders { get; }
    }
}
