namespace Infrastructure.SaveSystem
{
    [System.Serializable]
    public class RunnerSaveData : ISaveableData
    {
        public int highScore;
        public bool unlockedSkin;
    }

    [System.Serializable]
    public class Match3SaveData : ISaveableData
    {
        public int maxLevel;
        public int starsCollected;
    }

}