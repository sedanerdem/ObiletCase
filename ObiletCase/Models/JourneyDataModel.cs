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
        
        public string OriginName { get; set; }

        [JsonProperty("destination-id")]
        public long DestinationId { get; set; }

        public string DestinationName { get; set; }

        [JsonProperty("departure-date")]
        public string DepartureDate { get; set; }

        public Journey Journey { get; set; }
    }

    public class Journey
    {
        public string Origin { get; set; }

        public string Destination { get; set; }

        public DateTime Departure { get; set; }

        public DateTime Arrival { get; set; }

        public string Currency { get; set; }

        public string Duration { get; set; }

        [JsonProperty("internet-price")]
        public double InternetPrice { get; set; }

        public string Description { get; set; }

        [JsonProperty("partner-name")]
        public string PartnerName { get; set; }
    }

}