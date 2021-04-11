using Domain.Models;
using System;
using System.Threading.Tasks;

namespace Persistence.UserRepo
{
    public class UserRepository : IUserWriter
    {
        Task<User> IUserWriter.CreateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
