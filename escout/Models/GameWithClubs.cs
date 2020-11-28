namespace escout.Models
{
    public class GameWithClubs
    {
        public Game game { get; set; }
        public Club homeClub { get; set; }
        public Club visitorClub { get; set; }
    }
}
