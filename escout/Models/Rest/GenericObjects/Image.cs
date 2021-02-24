﻿using Newtonsoft.Json;

namespace escout.Models.Rest.GenericObjects
{
    public class Image
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("tags")]
        public string Tags { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("updated")]
        public string Updated { get; set; }
    }
}
