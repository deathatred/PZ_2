using UnityEngine;

public class RoadSegment : MonoBehaviour
{
    [SerializeField] private Transform[] _obstacleSpawnPoints;
    [SerializeField] private float speed = 5f;

    private void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }
    public void Activate(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
