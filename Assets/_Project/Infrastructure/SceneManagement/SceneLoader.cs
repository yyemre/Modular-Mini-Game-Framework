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

        public void LoadSceneAsync(SceneReference sceneRef, bool showLoading = true, Action onComplete = null)
        {
            StartCoroutine(LoadRoutine(sceneRef, showLoading, onComplete));
        }

        private IEnumerator LoadRoutine(SceneReference sceneRef, bool showLoading = true, Action onComplete = null)
        {
            if (showLoading) yield return loadingScreen?.FadeIn();

            AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(
                sceneRef.sceneReference,
                LoadSceneMode.Single
            );
            if (showLoading)
            {
                while (!handle.IsDone)
                {
                    loadingScreen?.SetProgress(handle.PercentComplete);
                    yield return null;
                }
            }

            if (showLoading) yield return loadingScreen?.FadeOut();
            onComplete?.Invoke();
        }
    }

}