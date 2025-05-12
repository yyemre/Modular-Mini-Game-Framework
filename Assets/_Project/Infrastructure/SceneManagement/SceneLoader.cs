using System;
using Infrastructure.AssetManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Infrastructure.SceneManagement
{
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

        public void LoadSceneAsync(SceneReference sceneRef, Action onComplete = null)
        {
            StartCoroutine(LoadRoutine(sceneRef, onComplete));
        }

        private IEnumerator LoadRoutine(SceneReference sceneRef, Action onComplete = null)
        {
            loadingScreen?.Show();
            AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneRef.sceneReference, LoadSceneMode.Single);
            handle.Completed += (op) =>
            {
                loadingScreen?.Hide();
                onComplete?.Invoke();
            };
            
            // AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
            // op.allowSceneActivation = false;
            // float fakeProgress = 0f;
            // while (op.progress < 0.9f)
            //  // while (fakeProgress < 0.9f)
            // {
            //     loadingScreen?.SetProgress(op.progress);
            //     fakeProgress += Time.deltaTime;
            //     yield return null;
            // }
            //
            // loadingScreen?.SetProgress(1f);
            //
            // yield return new WaitForSeconds(0.6f);
            // op.allowSceneActivation = true;
            //
            // loadingScreen?.Hide();
            // onComplete?.Invoke();
            yield return handle;
        }
    }

}