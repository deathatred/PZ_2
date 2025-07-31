using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] private RoadSegment roadPrefab;
    [SerializeField] private int initialPoolSize = 7;
    [SerializeField] private float segmentLength = 10f;
    [SerializeField] private float despawnZ = -25f;

    private ObjectPool<RoadSegment> pool;
    private List<RoadSegment> activeSegments = new List<RoadSegment>();

    void Start()
    {
        pool = new ObjectPool<RoadSegment>(roadPrefab, initialPoolSize);

        for (int i = 0; i < initialPoolSize; i++)
        {
            SpawnRoad();
        }
    }

    void Update()
    {
        int countToSpawn = 0;
        for (int i = activeSegments.Count - 1; i >= 0; i--)
        {
            if (activeSegments[i].transform.position.z < despawnZ)
            {
                pool.ReturnToPool(activeSegments[i]);
                activeSegments.RemoveAt(i);
                countToSpawn++;
            }
        }
        for (int i = 0; i < countToSpawn; i++)
        {
            SpawnRoad();
        }
    }
    void SpawnRoad()
    {
        RoadSegment newSegment = pool.Get();
        float ZOffset = 0.001f;
        float spawnZ = activeSegments.Count > 0
            ? (activeSegments[activeSegments.Count - 1].transform.position.z+ ZOffset) + segmentLength
            : 0f;

        Vector3 spawnPos = new Vector3(0f, 0f, spawnZ);
        newSegment.Activate(spawnPos);
        newSegment.GetComponent<RoadObstacleSpawn>().Spawn();
        activeSegments.Add(newSegment);
    }
}
