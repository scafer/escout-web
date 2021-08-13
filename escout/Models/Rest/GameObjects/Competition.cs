﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace escout.Models.Rest.GameObjects
{
    public class Competition
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
        [StringLength(32, MinimumLength = 3, ErrorMessage = "Edition has to be longer.")]
        [JsonProperty("edition")]
        public string Edition { get; set; }

        [JsonProperty("sportId")]
        public int SportId { get; set; }

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
