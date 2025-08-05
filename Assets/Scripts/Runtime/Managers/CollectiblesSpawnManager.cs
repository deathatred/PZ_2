using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.HableCurve;

public class CoinsSpawnManager : MonoBehaviour
{
    [SerializeField] private Coin _coinSpawnPrefabs;
    private int _poolSize = 20;
    private float _segmentLength = 20f;
    private ObjectPool<Coin> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Coin>(_coinSpawnPrefabs, _poolSize);
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

        Coin coin = _pool.Get();
        coin.transform.position = worldPos;
        coin.transform.rotation = Quaternion.identity;
        coin.gameObject.SetActive(true);
        coin.transform.SetParent(segment);
    }

    public void RecycleAllFromSegment(Transform segment)
    {
        foreach (Transform child in segment)
        {
            Coin coin = child.GetComponent<Coin>();
            if (coin != null)
            {
                _pool.ReturnToPool(coin);
            }
        }
    }
    public void SetPool(ObjectPool<Coin> pool)
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
        GameEventBus.OnObstacleSpawn += GameEventBusOnObstacleSpawn;
    }
    private void UnsubscribeFromEvents()
    {
        RoadSegment.OnSegmentSpawned -= SpawnOnSegment;
        RoadSegment.OnSegmentDespawned -= RoadSegmentOnSegmentDespawned;
        GameEventBus.OnObstacleSpawn -= GameEventBusOnObstacleSpawn;
    }

    private void GameEventBusOnObstacleSpawn(Transform obstacleTransform)
    {
        float chance = 0.5f;

        if (chance < Random.value)
        {
            Vector3 coinPos = new Vector3(obstacleTransform.position.x,
                obstacleTransform.position.y + 1.3f,
                obstacleTransform.position.z);
            Coin coin = _pool.Get();
            coin.transform.position = coinPos;
            coin.transform.rotation = Quaternion.identity;
            coin.gameObject.SetActive(true);
            coin.transform.SetParent(obstacleTransform.parent);
        }
    }
}
