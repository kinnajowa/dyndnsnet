using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DynDNSNet.Models
{
    public class Zone
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public string kind { get; set; }
        public ICollection<RRSet> rrsets { get; set; }
        [JsonIgnore]
        public int serial { get; set; }
        [JsonIgnore]
        public int notified_serial { get; set; }
        public int edited_serial { get; set; }
        public ICollection<string> masters { get; set; }
        public bool dnssec { get; set; }
        public string nsec3param { get; set; }
        public bool nsec3narrow { get; set; }
        public bool presigned { get; set; }
        public string soa_edit { get; set; }
        public string soa_edit_api { get; set; }
        public bool api_rectify { get; set; }
        public string zone { get; set; }
        [JsonIgnore]
        public ICollection<string> nameservers { get; set; }
        public ICollection<string> master_tsig_key_ids { get; set; }
        public ICollection<string> slave_tsig_key_ids { get; set; }
    }
}