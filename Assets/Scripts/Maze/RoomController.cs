using System;
using System.Collections;
using System.Collections.Generic;
using Combat;
using Core;
using PlayerControl;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public List<Gate> gates = new List<Gate>();

    public List<EnemyHealthBehaviour> enemies = new List<EnemyHealthBehaviour>();

    private EnemiesSpawner _enemiesSpawner;

    private TriggerOverlap _triggerOverlap;

    public event Action<RoomController> OnRoomEntered;

    public event Action<RoomController> OnRoomCleared;

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

        _enemiesSpawner.InitializeRoomManager(this);
        
        _triggerOverlap.OnTrigger += RoomEntered;

        EnemyHealthBehaviour.OnEnemyDeath += RemoveEnemy;
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
        if(player && !_isClear)
        {
            _isClosed = true;
            OnRoomEntered?.Invoke(this);
        }
    }        
}
