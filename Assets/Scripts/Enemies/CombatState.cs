using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using Stats;
using UnityEngine;
using UnityEngine.AI;

public class CombatState : BaseState
{
    private EnemyController _enemyController;
    private float nextAttackTime = 0.0f;
    private int side;
    //private NavMeshSurface surface;
    private StatsBehaviour _statBehaviour;

    public CombatState(EnemyController enemy) : base(enemy.gameObject)
    {
        _enemyController = enemy;
        _statBehaviour = _enemyController.GetComponent<StatsBehaviour>();
    }

    private void Awake() {
        
    }

    public override Type Tick()
    {
        if(!_enemyController.CheckDistance(_statBehaviour.GetStatValue(StatType.AttackRange)))
        {
            return typeof(ChaseState);
        }

        TurnToFacePlayer();

        if (CanAttack())
        {
            Attack();
            _enemyController.Animator.SetFloat("Speed", 0);   
            side = ChangeSide();
        }
        else
        {
            _enemyController.Animator.SetFloat("Speed", 1);
            SideStep(side);
        } 

        return null;
    }

    public void Attack()
    {
        // UnityEngine.Object.Instantiate(_monster.MonsterStats.attackPrefab, transform.position + transform.forward.normalized
        //        + new Vector3(0, transform.lossyScale.y, 0), transform.rotation);

        //ATTACK

        //nextAttackTime = Time.time + _monster.MonsterStats.attackReload;
        _enemyController.Attack();
    }

    private bool CanAttack()
    {
        return (Time.time > nextAttackTime) ? true : false;
        
    }

    private void TurnToFacePlayer()
    {
        Vector3 direction = _enemyController.Player.transform.position - this.transform.position;
        direction.y = 0;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                    Quaternion.LookRotation(direction), 0.1f);
    }

    private void SideStep(int side)
    {
        transform.localPosition += transform.right
            * side * 3.0f * 0.5f * Time.deltaTime; //to change
    }

    private int ChangeSide()
    {
        System.Random rand = new System.Random();
        return rand.Next(0, 2) * 2 - 1;
    }

   

}
