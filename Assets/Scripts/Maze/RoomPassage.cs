using PlayerControl;
using UnityEngine;

namespace Maze
{
    public class RoomPassage : MonoBehaviour
    {
        [SerializeField] private int _tpDistance = 22;
        private GameObject _mainCamera;
        private RoomController _roomManager;

        private void Awake()
        {
            _mainCamera = GameObject.FindGameObjectWithTag("CameraRig");
        }
        public void Initialize(RoomController roomManager)
        {
            _roomManager = roomManager;
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
}


