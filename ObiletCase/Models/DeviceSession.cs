using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ObiletCase.Models
{
    public class DeviceSession
    {
        [JsonProperty("session-id")]
        public string SessionId { get; set; }

        [JsonProperty("device-id")]
        public string DeviceId { get; set; }
    }
}