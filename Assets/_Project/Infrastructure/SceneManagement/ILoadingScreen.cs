using System.Collections;

namespace Infrastructure.SceneManagement
{
    public interface ILoadingScreen
    {
        IEnumerator  FadeIn();
        IEnumerator  FadeOut();
    }
}