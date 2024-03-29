﻿using Newtonsoft.Json;

namespace escout.Models.Rest.GameObjects;

public class Favorite
{
    public Favorite()
    {
    }

    public Favorite(int id)
    {
        Id = id;
    }

    [JsonProperty("id")] public int Id { get; set; }

    [JsonProperty("userId")] public int UserId { get; set; }

    [JsonProperty("athleteId")] public int? AthleteId { get; set; }

    [JsonProperty("clubId")] public int? ClubId { get; set; }

    [JsonProperty("competitionId")] public int? CompetitionId { get; set; }

    [JsonProperty("gameId")] public int? GameId { get; set; }

    [JsonProperty("created")] public string Created { get; set; }

    [JsonProperty("updated")] public string Updated { get; set; }
}