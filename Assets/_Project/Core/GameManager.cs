using UnityEngine;
using Infrastructure.SceneManagement;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        // TODO: DI
        [SerializeField] private SceneLoader loader;
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void PlayRunner()
        {
            loader.LoadSceneAsync("EndlessRunnerGameplay");
        }
        
        [ContextMenu("Return to Main Menu")]
        public void ReturnToMainMenu()
        {
            loader.LoadSceneAsync("MainMenu");
        }
    }
}