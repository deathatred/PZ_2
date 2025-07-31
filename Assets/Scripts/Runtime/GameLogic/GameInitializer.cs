using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private Obstacle _obstaclePrefab;
    [SerializeField] private RoadObstacleSpawn _roadObstacleSpawn;
    [SerializeField] private int _obstaclePoolSize = 10;

    private ObjectPool<Obstacle> _obstaclePool;

    private void Awake()
    {
        _obstaclePool = new ObjectPool<Obstacle>(_obstaclePrefab, _obstaclePoolSize);
        _roadObstacleSpawn.SetPool(_obstaclePool);
    }
}
