using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using Stats;
using UnityEngine;
using UnityEngine.AI;

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

    public override Type Tick()
    {
        Debug.Log("Chase");
        
        _enemyController.Agent.speed = _statBehaviour.GetStatValue(StatType.Speed);

        _enemyController.Agent.stoppingDistance = _statBehaviour.GetStatValue(StatType.AttackRange); 

        if(!_enemyController.CheckDistance(_statBehaviour.GetStatValue(StatType.Awareness)))
        {
            return typeof(WanderState);
        }

        if(_enemyController.CheckDistance(_statBehaviour.GetStatValue(StatType.AttackRange)))
        {
            return typeof(CombatState);
        }

        FallowPlayer();

        return null;
    }

    public void FallowPlayer()
    {
        _enemyController.Agent.SetDestination(_enemyController.Player.transform.position);
        _enemyController.Animator.SetFloat("Speed", 1);
    }
}
