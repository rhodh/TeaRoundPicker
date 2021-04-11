using System;

namespace Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
}
