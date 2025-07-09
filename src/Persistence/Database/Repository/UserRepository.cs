using Application.Interfaces.API;
using AutoMapper;
using Domain;
using Domain.DTO.Database.Models;
using LinqToDB;
using Persistence.Database.DbContextFactory;

namespace Persistence.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        public UserRepository(IDbContextFactory dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public bool CheckPasswordAsync(User user, string password)
        {
            return user.Password == PasswordHasher.HashPassword(password);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            await using (var context = _dbContextFactory.Create())
            {
                var user = context.User
                    .LoadWith(x => x.Role)
                    .FirstOrDefault(x => x.Email == email);

                return user;
            }
        }
    }
}
