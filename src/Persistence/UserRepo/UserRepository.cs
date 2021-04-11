using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.DBModels;
using System;
using System.Threading.Tasks;

namespace Persistence.UserRepo
{
    public class UserRepository : IUserWriter,  IUserReader
    {
        private readonly TeaRoundPickerContext _context;
        private readonly IMapper _mapper;

        public UserRepository(TeaRoundPickerContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<User> CreateUser(User user)
        {
            var dbModel = _mapper.Map<UserDbModel>(user);
            var savedUser = await _context.Users.AddAsync(dbModel);
            await _context.SaveChangesAsync();
            return _mapper.Map<User>(savedUser.Entity);
        }

        public async Task<User> GetUser(Guid userId)
        {
            var user
                = await _context.Users.SingleOrDefaultAsync(x => x.Id == userId);
            return _mapper.Map<User>(user);
        }
    }
}
