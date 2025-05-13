using System.Collections;
using UnityEngine;

namespace Infrastructure.SceneManagement
{
    public class DefaultLoadingScreen : MonoBehaviour, ILoadingScreen
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float fadeDuration = 0.5f;
        [SerializeField] private UnityEngine.UI.Slider progressBar;
        [SerializeField] private UnityEngine.UI.Image alternativeProgressBar;

        public IEnumerator FadeIn()
        {
            gameObject.SetActive(true);
            yield return StartCoroutine(Fade(1f));
            canvasGroup.blocksRaycasts = true;
        }

        public IEnumerator FadeOut()
        {
            yield return StartCoroutine(Fade(0f));
            canvasGroup.blocksRaycasts = false;
            gameObject.SetActive(false);
        }
        
        private IEnumerator Fade(float targetAlpha)
        {
            float startAlpha = canvasGroup.alpha;
            float elapsed = 0f;

            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / fadeDuration);
                canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
                yield return null;
            }

            canvasGroup.alpha = targetAlpha;

        }
        public void SetProgress(float progress)
        {
            if (alternativeProgressBar != null)
                alternativeProgressBar.fillAmount = Mathf.Clamp01(progress);
            if (progressBar != null)
                progressBar.value = Mathf.Clamp01(progress);
        }
    }
}