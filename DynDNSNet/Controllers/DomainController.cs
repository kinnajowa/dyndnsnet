using System.Threading.Tasks;
using DynDNSNet.DbContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DynDNSNet.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class DomainController : Controller
    {
        private readonly SqlDbContext _context;

        public DomainController(SqlDbContext context)
        {
            _context = context;
        }
        
        [HttpGet("")]
        [AllowAnonymous]
        public async Task<IActionResult> ListDomains()
        {
            return View(await _context.Domains.ToListAsync());
        }
    }
}