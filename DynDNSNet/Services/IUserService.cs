using System.Collections.Generic;
using System.Threading.Tasks;
using DynDNSNet.Entities;

namespace DynDNSNet.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> getAll();
        Task<User> getUser(string username);
    }
}