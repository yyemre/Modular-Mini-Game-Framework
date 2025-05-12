using System;

namespace Infrastructure.SceneManagement
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class SceneLoader : MonoBehaviour, ISceneLoader
    {
        [SerializeField] private DefaultLoadingScreen loadingScreen;
        public static SceneLoader Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        public void LoadSceneAsync(string sceneName, Action onComplete = null)
        {
            StartCoroutine(LoadRoutine(sceneName, onComplete));
        }

        private IEnumerator LoadRoutine(string sceneName, Action onComplete = null)
        {
            loadingScreen?.Show();
            
            AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
            op.allowSceneActivation = false;
            float fakeProgress = 0f;
            while (op.progress < 0.9f)
             // while (fakeProgress < 0.9f)
            {
                loadingScreen?.SetProgress(op.progress);
                fakeProgress += Time.deltaTime;
                yield return null;
            }

            loadingScreen?.SetProgress(1f);

            yield return new WaitForSeconds(0.6f);
            op.allowSceneActivation = true;

            loadingScreen?.Hide();
            onComplete?.Invoke();
        }
    }

}