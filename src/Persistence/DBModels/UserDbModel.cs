using System;

namespace Persistence.DBModels
{
    public class UserDbModel
    {
        public Guid Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
}
