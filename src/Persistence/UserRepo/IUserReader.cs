using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.UserRepo
{
    public interface IUserReader
    {
        Task<User> GetUser(Guid userId);
        Task<IEnumerable<User>> GetUsers(IEnumerable<Guid> userIds);
    }
}
