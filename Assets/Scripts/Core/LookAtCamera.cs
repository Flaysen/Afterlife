using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform _cameraTransform;
    private Transform _transform;

    private void Awake()
    {
        _cameraTransform = FindObjectOfType<Camera>().GetComponent<Transform>();
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        _transform.LookAt(_cameraTransform.position, Vector3.up);
    }
}
