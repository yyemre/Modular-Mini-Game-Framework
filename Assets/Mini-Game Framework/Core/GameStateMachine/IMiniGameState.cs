namespace Core
{
    public interface IMiniGameState
    {
        void Enter();
        void Tick();
        void Exit();
    }
}