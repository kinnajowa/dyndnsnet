using System;
using System.Collections.Generic;

#nullable disable

namespace DynDNSNet.Entities
{
    public partial class Domainuser
    {
        public int DomainsId { get; set; }
        public string UsersUsername { get; set; }
        public int Id { get; set; }

        public virtual Domain Domains { get; set; }
        public virtual User UsersUsernameNavigation { get; set; }
    }
}
