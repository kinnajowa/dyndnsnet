using System;
using System.Collections.Generic;

#nullable disable

namespace DynDNSNet.Entities
{
    public partial class Domain
    {
        public Domain()
        {
            Domainusers = new HashSet<Domainuser>();
        }

        public int Id { get; set; }
        public string Hostname { get; set; }
        public string Ip { get; set; }
        public string IpV6 { get; set; }

        public virtual ICollection<Domainuser> Domainusers { get; set; }
    }
}
