using UnityEngine;

public class RoadObstacleSpawn : MonoBehaviour
{
    [SerializeField] private ObjectPool<Obstacle> _obstaclePool;
    [SerializeField] private Obstacle _obstacleSpawnPrefab;
    private int _poolSize = 20;
    private float _chance = 0.1f;
    private float _segmentLength = 20f;

    private ObjectPool<Obstacle> pool;

    private void Awake()
    {
        pool = new ObjectPool<Obstacle>(_obstacleSpawnPrefab,_poolSize);
    }
    public void Spawn()
    {
        if (Random.value > _chance)
            return;

        float randomX = 2 * Random.Range(-1, 2);
        float randomZ = Random.Range(0f, _segmentLength);

        Vector3 localOffset = new Vector3(randomX, 1f, randomZ);
        Vector3 worldPos = transform.position + localOffset;

        Obstacle obj = _obstaclePool.Get();
        obj.transform.position = worldPos;
        obj.transform.rotation = Quaternion.identity;
        obj.transform.SetParent(transform);
    }
    public void SetPool(ObjectPool<Obstacle> pool)
    {
        _obstaclePool = pool;
    }
}
