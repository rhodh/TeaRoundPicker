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
        private readonly IUserReader _userReader;

        public UserService(IUserWriter userWriter, IUserReader userReader)
        {
            _userWriter = userWriter ?? throw new ArgumentNullException(nameof(userWriter));
            _userReader = userReader ?? throw new ArgumentNullException(nameof(userReader));
        }

        public async Task<User> CreateUser(UserDto user)
        {
            User newUser = new(Guid.NewGuid(), user.FirstName, user.LastName);
            return await _userWriter.CreateUser(newUser);
        }

        public async Task<User> GetUser(Guid userId)
        {
            User user = await _userReader.GetUser(userId) ?? throw new Exception();
            return user;
        }

    }
}
