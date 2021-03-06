﻿using Newtonsoft.Json;

namespace escout.Models.Rest.GameObjects
{
    public class GameWithClubs
    {
        [JsonProperty("game")]
        public Game Game { get; set; }

        [JsonProperty("homeClub")]
        public Club HomeClub { get; set; }

        [JsonProperty("visitorClub")]
        public Club VisitorClub { get; set; }
    }
}
