namespace ObiletCase.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class SessionResponse
    {
        public string Status { get; set; }

        public DeviceSession Data { get; set; }

        public string Message { get; set; }    
    }
}
