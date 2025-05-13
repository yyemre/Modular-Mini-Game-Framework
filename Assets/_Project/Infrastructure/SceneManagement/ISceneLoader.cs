using System;
using Infrastructure.AssetManagement;

namespace Infrastructure.SceneManagement
{
    public interface ISceneLoader
    {
        void LoadSceneAsync(SceneReference sceneRef, bool showLoading = true, Action onComplete = null);
    }
}