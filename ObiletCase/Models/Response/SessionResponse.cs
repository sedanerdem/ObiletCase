using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ObiletCase.Models
{
    public class SessionResponse
    {
        public string Status { get; set; }
        public DeviceSession Data { get; set; }
        public string Message { get; set; }    
    }
}
