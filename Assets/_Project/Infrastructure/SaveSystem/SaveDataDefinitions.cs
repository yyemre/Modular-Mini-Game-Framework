namespace Infrastructure.SaveSystem
{
    [System.Serializable]
    public class RunnerSaveData : ISaveableData
    {
        public int highScore;
    }

    [System.Serializable]
    public class Match3SaveData : ISaveableData
    {
        public int maxLevel;
    }

}