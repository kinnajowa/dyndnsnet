using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DynDNSNet.DbContexts;
using DynDNSNet.Entities;
using DynDNSNet.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DynDNSNet.Services
{
    public class UserService : IUserService
    {
        private SqlDbContext _dbContext;
        public UserService(SqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<User> Authenticate(string username, string password)
        {
            var user = await getUser(username);

            if (user == null) return null;

            if (user.Username != username) return null;
            
            return user.WithoutPassword();
        }

        public async Task<IEnumerable<User>> getAll()
        {
            return await Task.Run(() => _dbContext.Users);
        }

        public async Task<User> getUser(string username)
        {
            return await Task.Run(async () => await _dbContext.Users.FindAsync(username));
        }
    }
}