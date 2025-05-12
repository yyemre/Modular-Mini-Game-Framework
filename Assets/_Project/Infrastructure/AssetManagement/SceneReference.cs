using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.AssetManagement
{
    [CreateAssetMenu(menuName = "Game/MiniGame Scene Reference")]
    public class SceneReference : ScriptableObject
    {
        public string id;
        public AssetReference sceneReference;
    }
}