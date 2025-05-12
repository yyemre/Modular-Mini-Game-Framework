namespace Infrastructure.SaveSystem
{
    [System.Serializable]
    public class SaveContainer
    {
        public RunnerSaveData runnerData = new();
        public Match3SaveData match3Data = new();
    }

}