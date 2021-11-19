using System.Collections.Generic;

namespace DynDNSNet.Models
{
    public class RRSet
    {
        public string name { get; set; }
        public string type { get; set; }
        public int ttl { get; set; }
        public string changetype { get; set; }
        public ICollection<Record> records { get; set; }
        public ICollection<Comment> comments { get; set; }
    }
}