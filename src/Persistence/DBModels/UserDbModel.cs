using System;
using System.Collections.Generic;

namespace Persistence.DBModels
{
    public class UserDbModel
    {
        public Guid Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        virtual public IEnumerable<DrinkOrderDbModel> DrinkOrders { get; set; }
    }
}
