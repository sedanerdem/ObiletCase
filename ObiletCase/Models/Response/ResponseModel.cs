using Newtonsoft.Json;

namespace ObiletCase.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseModel<T>
    {
        public string Status { get; set; }
        
        public T Data { get; set; }

        [JsonProperty("user-message")]
        public string UserMessage { get; set; }
    }
}