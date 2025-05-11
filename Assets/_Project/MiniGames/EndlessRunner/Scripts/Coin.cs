using UnityEngine;

namespace MiniGames.EndlessRunner
{
    public class Coin : MonoBehaviour, ICollectable
    {
        [SerializeField] private int value = 1;

        public int Value => value;

        public void Collect()
        {
            Destroy(gameObject);
            
            // TODO: Event system
            FindFirstObjectByType<ScoreManager>().AddScore(value);
        }
    }
}