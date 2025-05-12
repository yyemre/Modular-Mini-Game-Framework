using System;

namespace Infrastructure.SceneManagement
{
    public interface ISceneLoader
    {
        void LoadSceneAsync(string sceneName, Action onComplete = null);
    }
}