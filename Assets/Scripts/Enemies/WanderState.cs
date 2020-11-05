using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using Stats;
using UnityEngine;

public class WanderState : BaseState
{

    private EnemyController _enemy;

    private StatsBehaviour _statsBehaviour;

    public WanderState(EnemyController enemy) : base(enemy.gameObject)
    {
        _enemy = enemy;
        _statsBehaviour = _enemy.GetComponent<StatsBehaviour>();
    }

    public override Type Tick()
    {
        if(_enemy.CheckDistance(_statsBehaviour.GetStatValue(StatType.Awareness)))
        {
            return typeof(ChaseState);
        }

        return null;
    }


}
