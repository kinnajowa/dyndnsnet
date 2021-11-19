using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DynDNSNet.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class AdminController : Controller
    {
        
    }
}