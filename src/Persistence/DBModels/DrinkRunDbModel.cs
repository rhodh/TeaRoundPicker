using Domain.Models;
using System;

namespace Persistence.DBModels
{
    public class DrinkRunDbModel
    {
        public Guid Id { get; set; }
        public Guid DrinkMakerId { get; set; }
        virtual public UserDbModel DrinkMaker { get; set; }
    }
}
