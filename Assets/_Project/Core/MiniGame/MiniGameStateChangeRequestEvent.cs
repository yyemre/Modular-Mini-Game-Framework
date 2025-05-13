namespace Core.MiniGame
{
    public class MiniGameStateChangeRequestEvent<T> where T : System.Enum
    {
        public T TargetState { get; }

        public MiniGameStateChangeRequestEvent(T targetState)
        {
            TargetState = targetState;
        }
    }
}