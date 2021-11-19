using System;
using System.Collections.Generic;

#nullable disable

namespace DynDNSNet.Entities
{
    public partial class User
    {
        public User()
        {
            Domainusers = new HashSet<Domainuser>();
        }

        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Domainuser> Domainusers { get; set; }
    }
}
