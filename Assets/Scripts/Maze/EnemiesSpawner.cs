using Combat;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private EnemyHealthBehaviour _monsterPrefab;

    private RoomController _roomController;
      
    private void Start()
    {
        _roomController.OnRoomEntered += SpawnMonsters;
    }

    public void InitializeRoomManager(RoomController roomController)
    {
        _roomController = roomController;
    }

    private void SpawnMonsters(RoomController roomController)
    {
      
        EnemyHealthBehaviour monster = Instantiate(_monsterPrefab,
                    transform.position + _monsterPrefab.transform.localPosition,
                    Quaternion.identity);

        roomController.enemies.Add(monster);
    
        _roomController.OnRoomEntered -= SpawnMonsters;
    }
}
