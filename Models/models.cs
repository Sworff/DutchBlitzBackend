namespace DutchBlitzBackend.Models
{
    public class Player
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public int TotalScore { get; set; }
        public Score? Score { get; set; }
    }

    public class Score {
        public int Dutch { get; set; }
        public int Blitz { get; set; }
        public int RoundScore { get; set; }
    }

    public class Round
    {
        public required string Id { get; set; }
        public int RoundNumber {  get; set; }
        public List<Player> Players { get; set; } = [];
    }

    public class Game
    {
        public required string Id { get; set; }
        public int WinningScore { get; set; } = 75;
        public List<Player> Players { get; set; } = [];
        public List<Round> Rounds { get; set; } = [];
        public bool IsGameOver { get; set; } = false;

    }
}
