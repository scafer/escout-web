using Newtonsoft.Json;

namespace escout.Models.Rest.GameObjects
{
    public class CompetitionBoardWithClub
    {
        [JsonProperty("competition")]
        public CompetitionBoard Competition { get; set; }

        [JsonProperty("club")]
        public Club Club { get; set; }

        public CompetitionBoardWithClub(CompetitionBoard competition, Club club)
        {
            Competition = competition;
            Club = club;
        }
    }
}
