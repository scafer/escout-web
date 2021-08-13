﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace escout.Models.Rest.GameObjects
{
    public class GameData
    {
        [JsonProperty("game")]
        public Game Game { get; set; }

        [JsonProperty("sport")]
        public Sport Sport { get; set; }

        [JsonProperty("clubs")]
        public List<Club> Clubs { get; set; }

        [JsonProperty("athletes")]
        public List<Athlete> Athletes { get; set; }

        [JsonProperty("competition")]
        public Competition Competition { get; set; }

        [JsonProperty("events")]
        public List<Event> Events { get; set; }

        [JsonProperty("gameEvents")]
        public List<GameEvent> GameEvents { get; set; }
    }
}
