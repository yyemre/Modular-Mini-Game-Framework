using UnityEngine;

namespace Core
{
    public abstract class MiniGameBase : MonoBehaviour
    {
        public abstract void Initialize();
        public abstract void StartGame();
        public abstract void EndGame();
    }
}