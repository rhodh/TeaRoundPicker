using Domain.Models;
using System.Threading.Tasks;

namespace Persistence.UserRepo
{
    public interface IUserWriter
    {
        Task<User> CreateUser(User user);
    }
}
