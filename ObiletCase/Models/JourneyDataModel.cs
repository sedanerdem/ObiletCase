using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ObiletCase.Models.Request
{
    public class JourneyDataModel
    {
        [JsonProperty("origin-id")]
        public long OriginId { get; set; }

        [JsonProperty("destination-id")]
        public long DestinationId { get; set; }

        [JsonProperty("departure-date")]
        public DateTime DepartureDate { get; set; }

        [JsonProperty("partner-name")]
        public string PartnerName { get; set; }

    }
}