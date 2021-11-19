using System;
using System.Linq;
using System.Threading.Tasks;
using DynDNSNet.DbContexts;
using DynDNSNet.Entities;
using DynDNSNet.Models;
using DynDNSNet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DynDNSNet.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace DynDNSNet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = "BasicAuthentication")]
    public class NicController : ControllerBase
    {
        private readonly SqlDbContext _dbContext;
        private readonly IApiService _apiService;

        public NicController(SqlDbContext dbContext, IApiService apiService)
        {
            _dbContext = dbContext;
            _apiService = apiService;
        }
        
        [HttpGet("update")]
        public async Task<IActionResult> GetUpdate([FromQuery] Update update)
        {
            var username = HttpContext.User.Identity.Name;
            Domain domain = null;
            if (String.IsNullOrEmpty(update.Hostname))
            {
                var rec = await _dbContext.Domainusers.Where(r => r.UsersUsername.Equals(username)).FirstOrDefaultAsync();
                if (rec == null) return BadRequest(ReturnCodes.Nohost.ToString());

                domain = await _dbContext.Domains.FindAsync(rec.DomainsId);
            }
            else
            {
                domain = await _dbContext.Domains.Where(d => d.Hostname.Equals(update.Hostname)).FirstOrDefaultAsync();
            }
            
            if (domain == null) return BadRequest(ReturnCodes.Nohost.ToString());

            var isPermitted =
                _dbContext.Domainusers.Any(p => p.DomainsId == domain.Id && p.UsersUsername.Equals(username));
            if (!isPermitted) return BadRequest(ReturnCodes.Nohost.ToString());

            if (String.IsNullOrEmpty(update.MyIp) && String.IsNullOrEmpty(update.MyIpV6)) return Ok(ReturnCodes.Nochg.ToString());

            domain.Ip = update.MyIp;
            domain.IpV6 = update.MyIpV6;

            var success = await _apiService.UpdateDomain(domain.Hostname, "", domain.Ip, domain.IpV6);

            if (success)
            {
                await _dbContext.SaveChangesAsync();

                return Ok(ReturnCodes.Good.ToString());
            }

            return BadRequest(ReturnCodes.Dnserr.ToString());
        }
    }
}