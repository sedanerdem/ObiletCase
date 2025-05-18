using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ObiletCase.Models
{
    public class ResponseModel<T>
    {
        public string Status { get; set; }
        public T Data { get; set; }
    }


}