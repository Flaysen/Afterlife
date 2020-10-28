using System.Collections.Generic;
using UnityEngine;
using Stats;
using Projectiles;
using Resource;
using System;

namespace Combat
{
    [RequireComponent(typeof(IAttackInvoker), (typeof(StatsBehaviour)))]
    public class AttackHandler : MonoBehaviour
    {
        private List<Turret> _turrets = new List<Turret>();
        
        [SerializeField] private ProjectilePool _projectilePool;

        [SerializeField] private Animator _animator;

        private IAttackInvoker _attackInvoker;

        private StatsBehaviour _stats;

        private IDamagable _avoidTarget;

        private float _nextAttackTime;

        public event Action OnProjectileFired;

        private void Awake()
        {
            _stats = GetComponent<StatsBehaviour>();

            _attackInvoker = GetComponent<IAttackInvoker>();

            _avoidTarget = GetComponent<IDamagable>();

            _attackInvoker.OnAttack += HandleFire;

            foreach(Turret turret in GetComponentsInChildren<Turret>())
            {
                _turrets.Add(turret);            
            }
        }

        private void HandleFire()
        {
            if(Time.time > _nextAttackTime)
            {
                SpawnProjectiles();

                OnProjectileFired?.Invoke();

                _nextAttackTime = Time.time + _stats.GetStatValue(StatType.AttackRatio);
            }    
        }

        private void SpawnProjectiles()
        {
            int activeTurrets = (int)_stats.GetStatValue(StatType.ProjectileCount);

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




