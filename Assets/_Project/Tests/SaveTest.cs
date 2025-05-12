using Infrastructure.SaveSystem;
using UnityEngine;
using Zenject;
namespace Tests
{
    public class SaveTest : MonoBehaviour
    {
        [Inject] private ISaveSystem _saveSystem;

        [ContextMenu("Save Dummy Data")]
        public void SaveDummy()
        {
            var save = _saveSystem.Load();
            save.runnerData.highScore = 1234;
            save.match3Data.maxLevel = 5;
            _saveSystem.Save(save);

            Debug.Log("✅ Dummy save data written.");
        }

        [ContextMenu("Load And Print")]
        public void LoadAndPrint()
        {
            var save = _saveSystem.Load();
            Debug.Log($"🏁 Runner High Score: {save.runnerData.highScore}");
            Debug.Log($"🧩 Match3 Max Level: {save.match3Data.maxLevel}");
        }
    }
}