using System;

namespace Domain.Models
{
    public class User
    {
        public User(Guid id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        public Guid Id { get; }
        public string LastName { get; }
        public string FirstName { get; }
    }
}
