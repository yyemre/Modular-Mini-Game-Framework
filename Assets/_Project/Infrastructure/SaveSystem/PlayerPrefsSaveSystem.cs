using UnityEngine;

namespace Infrastructure.SaveSystem
{
    public class PlayerPrefsSaveSystem : ISaveSystem
    {
        private const string SaveKey = "game_save";

        public void Save(SaveContainer container)
        {
            string json = JsonUtility.ToJson(container);
            PlayerPrefs.SetString(SaveKey, json);
            PlayerPrefs.Save();
        }

        public SaveContainer Load()
        {
            if (!PlayerPrefs.HasKey(SaveKey))
                return new SaveContainer();

            string json = PlayerPrefs.GetString(SaveKey);
            return JsonUtility.FromJson<SaveContainer>(json);
        }
    }
}