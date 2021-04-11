using Domain.Dto;
using Domain.Models;
using Persistence.UserRepo;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserWriter _userWriter;

        public UserService(IUserWriter userWriter)
        {
            _userWriter = userWriter ?? throw new ArgumentNullException(nameof(userWriter));
        }

        public async Task<User> CreateUser(UserDto user)
        {
            User newUser = new(Guid.NewGuid(), user.FirstName, user.LastName);
            return await _userWriter.CreateUser(newUser);
        }

        public Task<User> GetUser(Guid userId)
        {
            return Task.FromResult(new User(userId, "bob", "jones")); ;
        }

    }
}
