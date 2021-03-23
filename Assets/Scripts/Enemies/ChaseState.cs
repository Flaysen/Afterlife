using System;
using Core;
using Enemies;
using Stats;

namespace Enemies
{
    public class ChaseState : BaseState
    {
        private EnemyController _enemyController;
        private StatsBehaviour _statBehaviour;
        private float _speed;
        private float _awareness; 

        public ChaseState(EnemyController monster) : base(monster.gameObject)
        {
            _enemyController = monster;
            _statBehaviour = _enemyController.GetComponent<StatsBehaviour>();
        }
        public override Type StateUpdate()
        {       
            _enemyController.Agent.speed = _statBehaviour.GetStatValue(StatType.Speed);
            _enemyController.Agent.stoppingDistance = _statBehaviour.GetStatValue(StatType.AttackRange); 
            _enemyController.Animator.SetFloat("Speed", 1.0f);
            _enemyController.Animator.SetBool("isAttacking", false);

            if(!_enemyController.CheckDistance(_statBehaviour.GetStatValue(StatType.Awareness)))
            {
                return typeof(WanderState);
            }

            if(_enemyController.CheckDistance(_statBehaviour.GetStatValue(StatType.AttackRange)))
            {
                _enemyController.Animator.SetFloat("Speed", 0.0f);
                return typeof(CombatState);
            }

            FallowPlayer();
            return null;
        }
        public void FallowPlayer()
        {
            _enemyController.Agent.SetDestination(_enemyController.Player.transform.position);
        }
    }
}

