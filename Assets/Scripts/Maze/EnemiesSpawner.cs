using System.Collections.Generic;
using Combat;
using UnityEngine;
using Unity;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private EnemyHealthBehaviour [] _enemiesPrefabs;

    private RoomController _roomController;
      
    private void Awake()
    {
        _roomController = GetComponentInParent<RoomController>();
        _roomController.OnRoomEntered += SpawnMonsters;
    }

    // public void Initialize(RoomController roomController)
    // {
    //     _roomController = roomController;
    // }

    private void SpawnMonsters(RoomController roomController)
    {
        EnemyHealthBehaviour enemyToSpawn = Instantiate(
            _enemiesPrefabs[Random.Range(0, _enemiesPrefabs.Length)],
            transform.position,
            Quaternion.identity);
                    
        roomController.enemies.Add(enemyToSpawn);
    
        _roomController.OnRoomEntered -= SpawnMonsters;
    }
}
