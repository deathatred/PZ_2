using System;
using UnityEngine;

public class RoadSegment : MonoBehaviour
{
    [SerializeField] private Transform[] _obstacleSpawnPoints;
    [SerializeField] private float speed = 5f;

    public static event Action<Transform> OnSegmentSpawned;
    public static event Action<Transform> OnSegmentDespawned;

    private void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
    public void Activate(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
        OnSegmentSpawned?.Invoke(transform);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        OnSegmentDespawned?.Invoke(transform);
    }
}
