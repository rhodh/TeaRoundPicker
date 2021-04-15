using System;
using System.Collections.Generic;

namespace Persistence.DBModels
{
    public class DrinkRunDbModel
    {
        public Guid Id { get; set; }
        public Guid DrinkMakerId { get; set; }
        virtual public UserDbModel DrinkMaker { get; set; }
        virtual public IEnumerable<DrinkOrderDbModel> DrinkOrders { get;set;}
    }
}
