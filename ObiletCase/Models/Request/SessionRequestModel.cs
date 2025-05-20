using ObiletCase.Models.Request;

namespace ObiletCase.Models
{
    public class SessionRequestModel
    {
        public long Type { get; set; }
        public Connection Connection { get; set; }
        public Browser Browser { get; set; }
    }
}