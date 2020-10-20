using PlayerControl;
using UnityEngine;

public class RoomPassage : MonoBehaviour
{
    [SerializeField] private int _tpDistance;

    private GameObject _mainCamera;

    private void Awake()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("CameraRig");
        _tpDistance = 22;
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if(player)
        {
            Vector3 tpVector = this.transform.forward * _tpDistance;
            other.transform.position += tpVector;
            _mainCamera.transform.position += tpVector;
        }
    }
  
}
