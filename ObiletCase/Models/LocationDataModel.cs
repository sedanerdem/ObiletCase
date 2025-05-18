using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ObiletCase.Models.Response
{
    public class LocationDataModel
    {
        public int Id { get; set; }

        [JsonProperty("parent-id")]
        public int ParentId { get; set; }

        public string Type { get; set; }
        public string Name { get; set; }

        [JsonProperty("geo-location")]
        public GeoLocation GeoLocation { get; set; }
        public int Zoom { get; set; }

        [JsonProperty("city-id")]
        public int CityId { get; set; }

        [JsonProperty("city-name")]
        public string CityName { get; set; }

        [JsonProperty("country-name")]
        public string CountryName { get; set; }

        [JsonProperty("long-name")]
        public string LongName { get; set; }

        [JsonProperty("is-city-center")]
        public bool IsCityCenter { get; set; }

        public string Keywords { get; set; }
    }

    public class GeoLocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Zoom { get; set; }
    }
}


