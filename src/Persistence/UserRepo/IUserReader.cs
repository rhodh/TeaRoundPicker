using Domain.Models;
using System;
using System.Threading.Tasks;

namespace Persistence.UserRepo
{
    public interface IUserReader
    {
        Task<User> GetUser(Guid userId);
    }
}
