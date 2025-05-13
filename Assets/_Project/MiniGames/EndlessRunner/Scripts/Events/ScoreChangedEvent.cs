namespace MiniGames.EndlessRunner
{
    public class ScoreChangedEvent
    {
        public int Score;

        public ScoreChangedEvent(int score)
        {
            Score = score;
        }
    }

}