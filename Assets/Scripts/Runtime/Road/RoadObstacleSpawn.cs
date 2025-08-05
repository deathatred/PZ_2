using UnityEngine;

public class RoadObstacleSpawn : MonoBehaviour
{
    [SerializeField] private Obstacle _obstacleSpawnPrefab;
    private int _poolSize = 20;
    private float _segmentLength = 20f;

    private ObjectPool<Obstacle> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Obstacle>(_obstacleSpawnPrefab, _poolSize);
    }
    private void OnEnable()
    {
        SubscribeToEvents();
    }
    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }
    public void SpawnOnSegment(Transform segment)
    {
        float randomX = 2 * Random.Range(-1, 2);
        float randomZ = Random.Range(0f, _segmentLength);

        Vector3 localOffset = new Vector3(randomX, 0.5f, randomZ);
        Vector3 worldPos = segment.position + localOffset;

        Obstacle obstacle = _pool.Get();
        obstacle.transform.position = worldPos;
        obstacle.transform.rotation = Quaternion.identity;
        obstacle.gameObject.SetActive(true);
        obstacle.transform.SetParent(segment);
        GameEventBus.SpawnObstacle(obstacle.transform);
    }

    public void RecycleAllFromSegment(Transform segment)
    {
        foreach (Transform child in segment)
        {
            Obstacle obstacle = child.GetComponent<Obstacle>();
            if (obstacle != null)
            {
                _pool.ReturnToPool(obstacle);   
            }
        }
    }
    public void SetPool(ObjectPool<Obstacle> pool)
    {
        _pool = pool;
    }
    private void RoadSegmentOnSegmentDespawned(Transform segment)
    {
        RecycleAllFromSegment(segment);
    }
    private void SubscribeToEvents()
    {
        RoadSegment.OnSegmentSpawned += SpawnOnSegment;
        RoadSegment.OnSegmentDespawned += RoadSegmentOnSegmentDespawned;
    }
    private void UnsubscribeFromEvents()
    {
        RoadSegment.OnSegmentSpawned -= SpawnOnSegment;
        RoadSegment.OnSegmentDespawned -= RoadSegmentOnSegmentDespawned;
    }
}
