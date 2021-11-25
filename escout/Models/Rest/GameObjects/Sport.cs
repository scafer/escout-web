﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace escout.Models.Rest.GameObjects
{
    public class Sport
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("imageId")]
        public string ImageId { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("updated")]
        public string Updated { get; set; }

        [JsonProperty("displayOptions")]
        public Dictionary<string, string> DisplayOptions { get; set; }
    }
}
