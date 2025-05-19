using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ObiletCase.Constants
{
    /// <summary>
    /// 
    /// </summary>
    public class UrlPaths
    {
        /// <summary>
        /// GetSession UrlPath
        /// </summary>
        public const string GET_SESSION = "api/client/getsession";

        /// <summary>
        /// GetBusLocations UrlPath
        /// </summary>
        public const string GET_BUS_LOCATIONS = "api/location/getbuslocations";

        /// <summary>
        /// GetBusJourneys UrlPath
        /// </summary>
        public const string GET_BUS_JOURNEYS = "api/journey/getbusjourneys";
    }
}