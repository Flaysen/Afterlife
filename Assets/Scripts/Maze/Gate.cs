using Afterlife.Assets.WorldGeneration.Scripts;
using Core;
using UnityEngine;

namespace Maze 
{
    [RequireComponent(typeof(Collider))]
    public class Gate : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _gateParticles;
        private GameObject _door;
        private Collider _collider;
        private RoomController _roomController;
        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.enabled = false;
        }
        private void Start()
        {
            _door = transform.GetChild(0).gameObject;
            _door.gameObject.SetActive(false);
            _roomController.OnRoomEntered += CloseDoor;
            _roomController.OnRoomCleared += DisableDoors;
            _gateParticles.enableEmission = false;
        }
        public void Initialize(RoomController roomController)
        {
            _roomController = roomController;
        }
        private void DisableDoors(RoomController roomController)
        {                    
            _collider.enabled = false;
            _door.gameObject.SetActive(false);
            _gateParticles.loop = false;
            _roomController.OnRoomEntered -= CloseDoor;                   
        }
        private void CloseDoor(RoomController roomController)
        {            
            if(roomController.RoomType == RoomType.COMMON)  
            {
                _collider.enabled = true;
                _gateParticles.enableEmission = true;
                _door.gameObject.SetActive(true);   
            }                               
        }
    }
}

