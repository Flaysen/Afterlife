using Combat;
using UnityEngine;

namespace Maze
{
    public class EnemiesSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyHealthBehaviour [] _enemiesPrefabs;
        private RoomController _roomController;
        private void Awake()
        {
            _roomController = GetComponentInParent<RoomController>();
            _roomController.OnRoomEntered += SpawnMonsters;
        }
        private void SpawnMonsters(RoomController roomController)
        {
            EnemyHealthBehaviour enemyToSpawn = Instantiate(
                _enemiesPrefabs[Random.Range(0, _enemiesPrefabs.Length)],
                transform.position,
                Quaternion.identity);
                        
            roomController.Enemies.Add(enemyToSpawn);  
            _roomController.OnRoomEntered -= SpawnMonsters;
        }
    }
}


