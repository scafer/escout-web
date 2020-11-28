namespace escout.Models
{
    public class CompetitionBoardWithClub
    {
        public CompetitionBoard competition { get; set; }
        public Club club { get; set; }

        public CompetitionBoardWithClub(CompetitionBoard competition, Club club)
        {
            this.competition = competition;
            this.club = club;
        }
    }
}
