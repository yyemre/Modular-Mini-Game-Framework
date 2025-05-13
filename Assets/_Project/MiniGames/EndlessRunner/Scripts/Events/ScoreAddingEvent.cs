namespace MiniGames.EndlessRunner
{
    public class ScoreAddingEvent
    {
        public int PointToAdd;
        
        public ScoreAddingEvent(int point)
        {
            PointToAdd = point;
        }
    }
}