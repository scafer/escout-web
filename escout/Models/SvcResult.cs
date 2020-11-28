using Newtonsoft.Json;

namespace escout.Models
{
    public class SvcResult
    {
        [JsonProperty("errorCode")]
        public int ErrorCode { get; set; }
        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }
    }
}
