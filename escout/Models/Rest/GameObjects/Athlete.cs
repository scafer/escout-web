using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace escout.Models.Rest.GameObjects
{
    public class Athlete
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 3, ErrorMessage = "Name has to be longer.")]
        [JsonProperty("name")]
        public string Name { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 3, ErrorMessage = "Full Name has to be longer.")]
        [JsonProperty("fullname")]
        public string Fullname { get; set; }

        [JsonProperty("birthDate")]
        public string BirthDate { get; set; }

        [JsonProperty("birthPlace")]
        public string BirthPlace { get; set; }

        [JsonProperty("citizenship")]
        public string Citizenship { get; set; }

        [Required]
        [JsonProperty("height")]
        public double Height { get; set; }

        [Required]
        [JsonProperty("weight")]
        public double Weight { get; set; }

        [Required]
        [JsonProperty("position")]
        public string Position { get; set; }

        [Required]
        [JsonProperty("positionKey")]
        public int PositionKey { get; set; }

        [JsonProperty("agent")]
        public string Agent { get; set; }

        [JsonProperty("currentInternational")]
        public string CurrentInternational { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("clubId")]
        public int? ClubId { get; set; }

        [JsonProperty("imageId")]
        public int? ImageId { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("updated")]
        public string Updated { get; set; }

        [JsonProperty("displayOptions")]
        public Dictionary<string, string> DisplayOptions { get; set; }
    }
}
