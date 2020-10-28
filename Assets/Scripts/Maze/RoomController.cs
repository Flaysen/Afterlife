using System;
using System.Collections;
using System.Collections.Generic;
using Afterlife.Assets.WorldGeneration.Scripts;
using Combat;
using Core;
using PlayerControl;
using UnityEngine;

public class RoomController : MonoBehaviour
{

    public RoomType RoomType;
    public List<Gate> gates = new List<Gate>();

    public List<EnemyHealthBehaviour> enemies = new List<EnemyHealthBehaviour>();

    private Minimap _minimap;

    private EnemiesSpawner _enemiesSpawner;

    private TriggerOverlap _triggerOverlap;

    public event Action<RoomController> OnRoomEntered;

    public event Action<RoomController> OnRoomCleared;

    public event Action<RoomController> OnRoomSpawn;

    public static event Action OnEnter;

    private bool _isClosed;

    private bool _isClear;

    private void Awake()
    {
        foreach(Gate gate in GetComponentsInChildren<Gate>())
        {
            gates.Add(gate);
            gate.Initialize(this);
        }

        _enemiesSpawner = GetComponent<EnemiesSpawner>();

        _triggerOverlap = GetComponent<TriggerOverlap>();

        _minimap = FindObjectOfType<Minimap>();

        _enemiesSpawner?.InitializeRoomManager(this);
        
        _triggerOverlap.OnTrigger += RoomEntered;

        EnemyHealthBehaviour.OnEnemyDeath += RemoveEnemy;

        _minimap.RegisterRoom(this);
    }

    private void RemoveEnemy(EnemyHealthBehaviour enemy)
    {
        if(_isClosed == true)
        {
            enemies.Remove(enemy);
            if (enemies.Count == 0)
            {
                _isClear = true;
                OnRoomCleared?.Invoke(this);
            }
        }      
    }

    public void RoomEntered(Collider collider)
    {
        PlayerController player = collider.GetComponent<PlayerController>();
        if(player)
        {
            _isClosed = true;
            OnRoomEntered?.Invoke(this);
            
        }
    }        
}
