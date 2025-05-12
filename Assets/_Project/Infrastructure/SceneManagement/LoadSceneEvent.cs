namespace Infrastructure.SceneManagement
{
    public struct LoadSceneEvent
    {
        public string SceneId;
        public LoadSceneEvent(string sceneId) => SceneId = sceneId;
    }

}