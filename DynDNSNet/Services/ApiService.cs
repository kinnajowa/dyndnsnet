using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DynDNSNet.Models;
using Microsoft.Extensions.Configuration;

namespace DynDNSNet.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(IConfiguration config)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("X-API-Key", config["DnsApi:Secret"]);
            _httpClient.BaseAddress = new Uri($"{config["DnsApi:Host"]}/api/v1/servers/localhost");
        }

        public async Task<bool> UpdateDomain(string domain, string name, string ip, string ipv6 = null)
        {
            var body =  await _httpClient.GetAsync($"zones/{domain}");
            var zone = (Zone) JsonSerializer.Deserialize(await body.Content.ReadAsStringAsync(), typeof(Zone));
            
            bool A = false;
            bool AAAA = false;

            var entry_name = string.IsNullOrEmpty(name) ? zone.id : $"{name}.{zone.id}";
            ICollection<RRSet> new_sets = new List<RRSet>();

            foreach (var set in zone.rrsets)
            {
                if (!set.name.Equals(entry_name)) continue;
                
                if (set.type.Equals("A"))
                {
                    set.records.First().content = ip;
                    set.changetype = "REPLACE";
                    new_sets.Add(set);
                    A = true;
                } else if (set.type.Equals("AAAA"))
                {
                    set.records.First().content = ipv6;
                    set.changetype = "REPLACE";
                    new_sets.Add(set);
                    AAAA = true;
                }
            }

            if (!A)
            {
                new_sets.Add(new RRSet { comments = new List<Comment>(), name = entry_name, records = new List<Record> {new Record {content = ip, disabled = false}}, ttl = 60, type = "A", changetype = "REPLACE"});
            }

            if (!AAAA && !string.IsNullOrEmpty(ipv6))
            {
                new_sets.Add(new RRSet { comments = new List<Comment>(), name = entry_name, records = new List<Record> {new Record {content = ipv6, disabled = false}}, ttl = 60, type = "AAAA", changetype = "REPLACE"});
            }

            zone.rrsets = new_sets;

            var patchBody = new StringContent(JsonSerializer.Serialize(zone), Encoding.UTF8, "application/json");
            var responsUpdate = await _httpClient.PatchAsync($"zones/{domain}", patchBody);
            var responseNotify = await _httpClient.PutAsync($"zones/{domain}/notify", null);
            return responseNotify.IsSuccessStatusCode & responsUpdate.IsSuccessStatusCode;
        }
    }
}