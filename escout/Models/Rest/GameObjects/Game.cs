﻿using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace escout.Models.Rest.GameObjects
{
    public class Game
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [Required]
        [JsonProperty("timeStart")]
        public string TimeStart { get; set; }

        [Required]
        [JsonProperty("timeEnd")]
        public string TimeEnd { get; set; }

        [JsonProperty("homeScore")]
        public int HomeScore { get; set; }

        [JsonProperty("visitorScore")]
        public int VisitorScore { get; set; }

        [JsonProperty("homePenaltyScore")]
        public int HomePenaltyScore { get; set; }

        [JsonProperty("visitorPenaltyScore")]
        public int VisitorPenaltyScore { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a home club")]
        [JsonProperty("homeId")]
        public int HomeId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a visitor club")]
        [JsonProperty("visitorId")]
        public int VisitorId { get; set; }

        [JsonProperty("competitionId")]
        public int? CompetitionId { get; set; }

        [JsonProperty("imageId")]
        public int? ImageId { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("updated")]
        public string Updated { get; set; }
    }
}
