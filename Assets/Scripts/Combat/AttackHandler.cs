using System.Collections.Generic;
using UnityEngine;
using Stats;
using Projectiles;
using Resource;
using System;

namespace Combat
{
    [RequireComponent(typeof(StatsBehaviour), typeof(IDamagable))]
    public class AttackHandler : MonoBehaviour
    {  
        [SerializeField] private ProjectilePool _projectilePool;
        [SerializeField] private Animator _animator;
        public event Action OnProjectileFired;
        private List<Turret> _turrets = new List<Turret>();
        private IAttackHandler _attackInvoker;
        private StatsBehaviour _stats;
        private IDamagable _avoidTarget;
        private float _nextAttackTime;
       
        private void Awake()
        {
            _stats = GetComponent<StatsBehaviour>();
            _attackInvoker = GetComponent<IAttackHandler>();
            _avoidTarget = GetComponent<IDamagable>();
            _attackInvoker.OnAttackTrigger += HandleAttack;
            _attackInvoker.OnAttackCancel += AttackCancel;
            PlayerHealthBehaviour.OnPlayerDeath += () => _nextAttackTime = float.PositiveInfinity;

            if(!_projectilePool) _projectilePool =
                GameObject.FindGameObjectWithTag("EnemyProjectilesPool").GetComponent<ProjectilePool>();

            foreach(Turret turret in GetComponentsInChildren<Turret>())
            {
                _turrets.Add(turret);            
            }
        }
        private void HandleAttack()
        {
            if(Time.time > _nextAttackTime)
            {
                SpawnProjectiles();
                OnProjectileFired?.Invoke();
                _nextAttackTime = Time.time + _stats.GetStatValue(StatType.AttackRatio);
                _animator.SetBool("IsAttacking", true);
            }                
        }
        private void AttackCancel()
        {
            _animator.SetBool("IsAttacking", false);
        }
        private void SpawnProjectiles()
        {
            int activeTurrets = (int)_stats.GetStatValue(StatType.ProjectilesCount);

            for (int i = 0; i < activeTurrets; i++)
            {
                Projectile projectile =  _projectilePool.Get();

                if(projectile)
                {
                    projectile.Initialize(_stats.ProjectileModifiers, _stats,
                     _turrets[i].transform, _avoidTarget);
                    
                    projectile.gameObject.SetActive(true);
                } 
            }     
        }         
    }
}




