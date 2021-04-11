using Domain.Models;
using System;
using System.Threading.Tasks;

namespace Persistence.UserRepo
{
    public class UserRepository : IUserWriter
    {
        public Task<User> CreateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
