using System.Collections.Generic;
using UnityEngine;

namespace MiniGames.EndlessRunner
{
    public class WorldOriginManager : MonoBehaviour
    {
        [Header("Floating Origin Settings")]
        [SerializeField] private Transform player;
        [SerializeField] private float resetThreshold = 1000;

        public List<Transform> worldObjects = new();

        public void RegisterWorldObject(Transform obj)
        {
            if (!worldObjects.Contains(obj))
                worldObjects.Add(obj);
        }
        private void Start()
        {
            InvokeRepeating(nameof(CheckFloatingOrigin), 0f, 2f);
        }
        private void CheckFloatingOrigin()
        {
            if (player.position.z >= resetThreshold)
            {
                ShiftWorld(-player.position.z);
            }
        }
        private void ShiftWorld(float offsetZ)
        {
            Vector3 offset = new(0, 0, offsetZ);

            var controller = player.GetComponent<CharacterController>();
            
            if (controller != null) controller.enabled = false;

            player.position += offset;
            
            if (controller != null) controller.enabled = true;
            
            for (int i = worldObjects.Count - 1; i >= 0; i--)
            {
                if (worldObjects[i] == null)
                {
                    worldObjects.RemoveAt(i);
                    continue;
                }

                worldObjects[i].position += offset;
            }
        }
    }

}