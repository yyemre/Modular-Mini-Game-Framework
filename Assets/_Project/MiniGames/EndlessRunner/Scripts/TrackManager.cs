using System.Collections.Generic;
using UnityEngine;

namespace MiniGames.EndlessRunner
{
    public class TrackManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform character;
        [SerializeField] private Transform SpawnPoint;
        [SerializeField] private GameObject[] segmentPrefabs;
        [SerializeField] private WorldOriginManager originManager;
        
        [Header("Track Settings")]
        [SerializeField] private int initialSegments = 5;
        [SerializeField] private float segmentLength = 30f;
        [SerializeField] private float recycleDistance = 60f;

        private List<GameObject> activeSegments = new();

        private void Start()
        {
            for (int i = 0; i < initialSegments; i++)
            {
                SpawnSegment();
            }
        }

        private void Update()
        {
            if (character.position.z - activeSegments[0].transform.position.z > recycleDistance)
            {
                RecycleSegment();
                SpawnSegment();
            }
        }

        private void SpawnSegment()
        {
            GameObject prefab = segmentPrefabs[Random.Range(0, segmentPrefabs.Length)];
            GameObject segment = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            if (activeSegments.Count > 0)
            {
                segment.transform.position = activeSegments[activeSegments.Count - 1].transform.position + new Vector3(0,0,segmentLength);
            }
            activeSegments.Add(segment);
            originManager.RegisterWorldObject(segment.transform);
        }

        private void RecycleSegment()
        {
            GameObject oldSegment = activeSegments[0];
            activeSegments.RemoveAt(0);
            Destroy(oldSegment);
        }

    }

}