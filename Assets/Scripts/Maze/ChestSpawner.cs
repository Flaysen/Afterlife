using UnityEngine;

namespace Maze
{
    [RequireComponent(typeof(RoomController))]
    public class ChestSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _chestPrefab;
        private RoomController _roomController;

        private void Awake()
        {
            _roomController = GetComponent<RoomController>();
        }
        private void Start()
        {          
            _roomController.OnLastEnemyKilled += SpawnChest;
        }
        public void SpawnChest(Transform spwanTransform)
        { 
            Debug.Log("Chest");
            Instantiate(_chestPrefab, spwanTransform.position, Quaternion.identity, transform);
        }  
    }
}


