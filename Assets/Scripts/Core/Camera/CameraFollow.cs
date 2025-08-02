using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset = new Vector3(0f, 2f, -5f); 
    [SerializeField] private float _smoothSpeed = 5f;

    private float _initialY;

    private void Start()
    {
        _initialY = transform.position.y;
    }
    private void LateUpdate()
    {
        if (_target == null) return;

        Vector3 targetPos = _target.position + _target.TransformDirection(_offset);
        targetPos.y = _initialY; 
        transform.position = Vector3.Lerp(transform.position, targetPos, _smoothSpeed * Time.deltaTime);

    }
}
