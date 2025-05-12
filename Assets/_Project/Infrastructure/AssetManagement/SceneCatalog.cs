using UnityEngine;
using System.Collections.Generic;

namespace Infrastructure.AssetManagement
{
    [CreateAssetMenu(menuName = "Game/Scene Catalog")]
    public class SceneCatalog : ScriptableObject
    {
        public List<SceneReference> scenes;

        public SceneReference GetSceneById(string id)
        {
            return scenes.Find(scene => scene.id == id);
        }
    }
}