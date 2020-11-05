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
        InitializeGates();
        //InitializeSpawners();

        _enemiesSpawner = GetComponent<EnemiesSpawner>();

        _triggerOverlap = GetComponent<TriggerOverlap>();

        _minimap = FindObjectOfType<Minimap>();
        
        EnemyHealthBehaviour.OnEnemyDeath += RemoveEnemy;

        _minimap.RegisterRoom(this);
    }

    private void Start()
    {
        _triggerOverlap.OnTrigger += RoomEntered;
    }

    private void InitializeGates()
    {
        foreach(Gate gate in GetComponentsInChildren<Gate>())
        {
            gates.Add(gate);
            gate.Initialize(this);
        }
    }

    private void InitializeSpawners()
    {
        foreach(EnemiesSpawner spawner in GetComponentsInChildren<EnemiesSpawner>())
        {
            //spawner.Initialize(this);
        }
    }

    private void RemoveEnemy(EnemyHealthBehaviour enemy)
    {
        Debug.Log("MONSTER_DEAD");
        if(_isClosed == true)
        {
            enemies.Remove(enemy);
            if (enemies.Count == 0)
            {
                Debug.Log("Clear");
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
