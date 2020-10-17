using UnityEngine;
using Stats;
using Resource;
using System;

namespace Combat
{
    [RequireComponent(typeof(StatsBehaviour))]
    public class EnemyHealthBehaviour : MonoBehaviour, IDamagable, IHealable
    {
        private StatsBehaviour _stats;

        public static event Action<float> OnDamageTaken;

        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; private set; }

        private void Start()
        {
            _stats = GetComponent<StatsBehaviour>();

            MaxHealth = _stats.GetStatValue(StatType.Health);

            CurrentHealth = MaxHealth;
        }

        public void TakeDamage(float damage)
        {
            CurrentHealth = (CurrentHealth - damage > 0) ?
                CurrentHealth - damage : 0;

            OnDamageTaken?.Invoke(damage);

            if (CurrentHealth <= 0) Destroy(gameObject);
        }

        public void Heal(float healValue)
        {
            CurrentHealth = (CurrentHealth + healValue < MaxHealth) ?
                CurrentHealth + healValue : MaxHealth; 
        }
    }

}

