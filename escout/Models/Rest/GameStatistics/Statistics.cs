using System.Collections.Generic;
using escout.Models.Rest.GameObjects;
using Newtonsoft.Json;

namespace escout.Models.Rest.GameStatistics
{
    public class Statistics
    {
        [JsonProperty("gameStats")]
        public List<GameStat> GameStats { get; set; }

        [JsonProperty("totalStats")]
        public List<TotalStat> TotalStats { get; set; }
    }

    public class GameStat
    {
        [JsonProperty("gameId")]
        public int GameId { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("eventId")]
        public int EventId { get; set; }
    }

    public class ClubStat
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("eventId")]
        public int EventId { get; set; }

        [JsonProperty("clubId")]
        public int ClubId { get; set; }
    }

    public class TotalStat
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("average")]
        public double Average { get; set; }

        [JsonProperty("median")]
        public double Median { get; set; }

        [JsonProperty("standardDeviation")]
        public double StandardDeviation { get; set; }

        [JsonProperty("eventId")]
        public int EventId { get; set; }
    }

    public class StatsTable
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("average")]
        public double Average { get; set; }

        [JsonProperty("median")]
        public double Median { get; set; }

        [JsonProperty("standardDeviation")]
        public double StandardDeviation { get; set; }

        [JsonProperty("eventId")]
        public int EventId { get; set; }

        [JsonProperty("eventName")]
        public string EventName { get; set; }

        [JsonProperty("eventKey")]
        public string EventKey { get; set; }

        public StatsTable(TotalStat stats, Event e)
        {
            Count = stats.Count;
            Average = stats.Average;
            Median = stats.Median;
            StandardDeviation = stats.StandardDeviation;
            EventId = stats.EventId;
            EventKey = e.Key;
            EventName = e.Name;
        }

        public StatsTable(ClubStat stats, Event e)
        {
            Count = stats.Count;
            EventId = stats.EventId;
            EventName = e.Name;
            EventKey = e.Key;
        }
    }
}
