using System.ComponentModel.DataAnnotations;

namespace DynDNSNet.Models
{
    public class AuthenticationModel
    {
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
    }
}