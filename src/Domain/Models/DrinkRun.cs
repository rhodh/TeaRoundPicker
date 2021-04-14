using System;

namespace Domain.Models
{
    public class DrinkRun
    {
        public DrinkRun(Guid id, User drinkMaker)
        {
            Id = id;
            DrinkMaker = drinkMaker;
        }

        public Guid Id { get;  }

        public User DrinkMaker { get; }
    }
}
