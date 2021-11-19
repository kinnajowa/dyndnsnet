using System;

namespace DynDNSNet.Models
{
    public class Update
    {
        public String Hostname { get; set; }
        public String MyIp { get; set; }
        public String MyIpV6 { get; set; }
    }
}