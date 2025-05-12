using Infrastructure.AssetManagement;
using UnityEngine;
using Infrastructure.SceneManagement;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        // TODO: DI
        [SerializeField] private SceneLoader loader;
        [SerializeField] private SceneReference runnerScene;
        [SerializeField] private SceneReference mainMenuScene;
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

        // TODO : daha modüler olacak. SO ile yönetilecek.
        public void PlayRunner()
        {
            loader.LoadSceneAsync(runnerScene);
        }
        
        [ContextMenu("Return to Main Menu")]
        public void ReturnToMainMenu()
        {
            loader.LoadSceneAsync(mainMenuScene);
        }
    }
}