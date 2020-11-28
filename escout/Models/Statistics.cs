using System.Collections.Generic;

namespace escout.Models
{
    public class Statistics
    {
        public List<GameStat> gameStats { get; set; }
        public List<TotalStat> totalStats { get; set; }
    }

    public class GameStat
    {
        public int gameId { get; set; }
        public int count { get; set; }
        public int eventId { get; set; }
    }

    public class ClubStat
    {
        public int count { get; set; }
        public int eventId { get; set; }
        public int clubId { get; set; }
    }

    public class TotalStat
    {
        public int count { get; set; }
        public double average { get; set; }
        public double median { get; set; }
        public double standardDeviation { get; set; }
        public int eventId { get; set; }
    }

    public class StatsTable
    {
        public int count { get; set; }
        public double average { get; set; }
        public double median { get; set; }
        public double standardDeviation { get; set; }
        public int eventId { get; set; }
        public string eventName { get; set; }
        public string eventKey { get; set; }

        public StatsTable(TotalStat stats, Event e)
        {
            count = stats.count;
            average = stats.average;
            median = stats.median;
            standardDeviation = stats.standardDeviation;
            eventId = stats.eventId;
            eventKey = e.Key;
            eventName = e.Name;
        }

        public StatsTable(ClubStat stats, Event e)
        {
            count = stats.count;
            eventId = stats.eventId;
            eventName = e.Name;
            eventKey = e.Key;
        }
    }
}
