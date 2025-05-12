using System.Collections;
using UnityEngine;

namespace Infrastructure.SceneManagement
{
    public class DefaultLoadingScreen : MonoBehaviour, ILoadingScreen
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float fadeDuration = 0.5f;
        [SerializeField] private UnityEngine.UI.Image progressBar;

        public virtual void Show()
        {
            gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(Fade(1));
        }

        public virtual void Hide()
        {
            StopAllCoroutines();
            StartCoroutine(Fade(0));
        }
        

        private IEnumerator Fade(float targetAlpha)
        {
            float startAlpha = canvasGroup.alpha;
            float elapsed = 0f;

            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeDuration);
                yield return null;
            }

            canvasGroup.alpha = targetAlpha;

        }
        public void SetProgress(float progress)
        {
            if (progressBar != null)
                progressBar.fillAmount = Mathf.Clamp01(progress);
        }
    }
}