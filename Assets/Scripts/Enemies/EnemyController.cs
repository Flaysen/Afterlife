using UnityEngine;
using Combat;
using System;
using Core;
using Stats;
using System.Collections.Generic;
using UnityEngine.AI;

namespace Enemies
{
    public class EnemyController : MonoBehaviour, IAttackHandler, IController
    {   
        [SerializeField] private Animator _animator;    
        public bool IsControlDisabled { get ; set; }   
        public Transform Player => _player;
        public Animator Animator => _animator;
        public NavMeshAgent Agent => _agent;
        public event Action OnAttackTrigger;
        public event Action OnAttackCancel;
        private Rigidbody _rigidbody;
        private Transform _player;   
        private StatsBehaviour _statsBehaviour;
        private NavMeshAgent _agent;
        private EnemyStateMachine _stateMachine;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _statsBehaviour = GetComponent<StatsBehaviour>();
            _player = FindObjectOfType<Player>().transform;
            _agent = GetComponent<NavMeshAgent>();
            _stateMachine = GetComponent<EnemyStateMachine>();
            InitializeStateMachine();
        }
        private void InitializeStateMachine()
        {
            var states  = new Dictionary<Type, BaseState>()
            {
                { typeof(WanderState), new WanderState(this) },
                { typeof(ChaseState), new ChaseState(this) },
                { typeof(CombatState), new CombatState(this) }
            };
            _stateMachine.SetStates(states);
        }
        public void Attack()
        {                   
            if(!IsControlDisabled)OnAttackTrigger?.Invoke();
        }
        public bool CheckDistance(float distance)
        {
            return (Vector3.Distance(_player.transform.position, this.transform.position) < distance) ? true : false;
        }   
    }
}

