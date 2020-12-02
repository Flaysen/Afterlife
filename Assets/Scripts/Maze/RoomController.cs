using System;
using System.Collections.Generic;
using Afterlife.Assets.WorldGeneration.Scripts;
using Combat;
using Core;
using PlayerControl;
using UnityEngine;

namespace Maze 
{
    public class RoomController : MonoBehaviour
    {
        public RoomType RoomType  { get; set; }
        public List<Gate> Gates = new List<Gate>();
        public List<EnemyHealthBehaviour> Enemies = new List<EnemyHealthBehaviour>();
        public event Action<RoomController> OnRoomEntered;
        public event Action<RoomController> OnRoomCleared;
        public event Action<Transform> OnLastEnemyKilled;
        public event Action<RoomController> OnRoomSpawn;
        public static event Action OnEnter;
        private Minimap _minimap;
        private EnemiesSpawner _enemiesSpawner;
        private TriggerOverlap _triggerOverlap;
        private ChestSpawner _chestSpawner;
        private bool _isClosed;
        private bool _isClear;
        private void Awake()
        { 
            InitializeGates();
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
                Gates.Add(gate);
                gate.Initialize(this);
            }
        }
        private void RemoveEnemy(EnemyHealthBehaviour enemy)
        {
            if(_isClosed == true)
            {
                Transform enemyTransform = enemy.transform;
                Enemies.Remove(enemy);
                if (Enemies.Count == 0 && _isClear == false)
                {                  
                    _isClear = true;
                    OnLastEnemyKilled?.Invoke(enemyTransform);
                    OnRoomCleared?.Invoke(this);
                }
            }      
        }
        public void RoomEntered(Collider collider)
        {
            Debug.Log("Enter");
            PlayerController player = collider.GetComponent<PlayerController>();
            if(player)
            {
                _isClosed = true;
                if(RoomType!= RoomType.ENTRY) OnRoomEntered?.Invoke(this);       
            }
        }      
    }
}

