using System.Threading.Tasks;

namespace DynDNSNet.Services
{
    public interface IApiService
    {
        public Task<bool> UpdateDomain(string domain, string name, string ip, string ipv6 = null);
    }
}