namespace Core.GameStateMachine
{
    public interface IMiniGameState
    {
        void Enter();
        void Tick();
        void Exit();
    }
}