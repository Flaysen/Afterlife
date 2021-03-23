using System;
using Core;
using Enemies;
using Stats;
using UnityEngine;

public class CombatState : BaseState
{
    private EnemyController _enemyController;
    private float _nextAttackTime = 0.0f;
    private int _side;
    private StatsBehaviour _statBehaviour;

    public CombatState(EnemyController enemy) : base(enemy.gameObject)
    {
        _enemyController = enemy;
        _statBehaviour = _enemyController.GetComponent<StatsBehaviour>();
    }
    public override Type StateUpdate()
    {
        if(!_enemyController.CheckDistance(_statBehaviour.GetStatValue(StatType.AttackRange)))
        {
            return typeof(ChaseState);
        }

        TurnToFacePlayer();

        if (CanAttack())
        {
            Attack();   
            _enemyController.Animator.SetBool("isAttacking", true);
            _side = ChangeSide();
        }
        else
        {
            SideStep(_side);
        } 

        return null;
    }

    public void Attack()
    {   
        _enemyController.Attack();
    }

    private bool CanAttack()
    {
        Debug.Log(Time.time > _nextAttackTime);
        return (Time.time > _nextAttackTime) ? true : false;    
    }
    private void TurnToFacePlayer()
    {
        Vector3 direction = _enemyController.Player.transform.position - this._transform.position;
        direction.y = 0;

        this._transform.rotation = Quaternion.Slerp(this._transform.rotation,
                                    Quaternion.LookRotation(direction), 0.1f);
    }
    private void SideStep(int side)
    {
        _transform.localPosition += _transform.right
            * side * 3.0f * 0.5f * Time.deltaTime;
    }
    private int ChangeSide()
    {
        System.Random rand = new System.Random();
        return rand.Next(0, 2) * 2 - 1;
    }
}
