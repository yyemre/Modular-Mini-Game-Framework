namespace Infrastructure.SceneManagement
{
    public struct LoadSceneEvent
    {
        public string SceneId;
        public bool ShowLoading;
        public LoadSceneEvent(string sceneId, bool showLoading = true)
        {
            SceneId = sceneId;
            ShowLoading = showLoading;
        }
    }

}