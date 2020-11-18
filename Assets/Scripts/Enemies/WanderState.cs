using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using Stats;
using UnityEngine;
using UnityEngine.AI;

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

    public override Type Tick()
    {   
         _enemyController.Agent.speed = _statsBehaviour.GetStatValue(StatType.Speed) / 3.0f;  

        if(transform.position == _nextPosition || _nextPosition == Vector3.zero)
        {        
            Debug.Log("NEW");
            _nextPosition = RandomNavmeshLocation(10.0f);     
            Debug.Log(_nextPosition); 
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
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 position = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1)) {
            position = hit.position;            
        }
        return position;
     }


}
