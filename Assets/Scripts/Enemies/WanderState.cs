using System;
using Core;
using Stats;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class WanderState : BaseState
    {
        private EnemyController _enemyController;
        private StatsBehaviour _statsBehaviour;
        private Vector3 _nextPosition;
        private float _restTime;
        private float _reachPosMoment;
        public WanderState(EnemyController enemy) : base(enemy.gameObject)
        {
            _enemyController = enemy;
            _statsBehaviour = _enemyController.GetComponent<StatsBehaviour>();
        }
        public override Type StateUpdate()
        {   
            _enemyController.Agent.speed = _statsBehaviour.GetStatValue(StatType.Speed) / 2.0f; 
             _enemyController.Animator.SetFloat("Speed", 0.5f); 

            if(_transform.position == _nextPosition || _nextPosition == Vector3.zero)
            {        
                _nextPosition = RandomNavmeshLocation(10.0f);     
            }

            _enemyController.Agent.SetDestination(_nextPosition);

            if(_enemyController.CheckDistance(_statsBehaviour.GetStatValue(StatType.Awareness)))
            {
                return typeof(ChaseState);
            }

            return null;
        }
        private Vector3 RandomNavmeshLocation(float radius)
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * radius;
            randomDirection += _transform.position;
            NavMeshHit hit;
            Vector3 position = Vector3.zero;
            if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1)) {
                position = hit.position;            
            }
            return position;
        }
    }
}

