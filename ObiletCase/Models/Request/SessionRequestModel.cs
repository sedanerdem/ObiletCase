using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ObiletCase.Models
{
    public class SessionRequestModel
    {
        public long Type { get; set; }
        public Connection Connection { get; set; }
        public Browser Browser { get; set; }
    }

    public class Connection
    {
        [JsonProperty("ip-address")]
        public string IpAddress { get; set; }

        [JsonProperty("port")]
        public string Port { get; set; }
    }
    public class Browser
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }
}