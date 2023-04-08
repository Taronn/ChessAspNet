namespace Chess.DTOs
{
    public class StatsDto
    {
        public int GamesPlayed { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
        public int Rating { get; set; }
        public int WinPercentage { get; set; }

        public int UpdateStats(int opponentRating, bool? isWinner)
        {
            double score;
            GamesPlayed++;
            if (isWinner == null)
            {
                Draws++;
                score = 0.5;
            }
            else if (isWinner == true)
            {
                Wins++;
                score = 1.0;
            }
            else
            {
                Losses++;
                score = 0;
            }
            double k = 32; // The K-factor determines the magnitude of the change in rating
            double winProbability = 1.0 / (1.0 + Math.Pow(10, (opponentRating - Rating) / 400.0)); // Calculate the win probability

            int oldRating = Rating;
            Rating = (int)Math.Round(Rating + k * (score - winProbability)); // Update the rating using the formula
            return oldRating;
        }
    }
}