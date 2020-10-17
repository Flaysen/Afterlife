using System;
using UnityEngine;
using Stats;

namespace Resource
{
    [RequireComponent(typeof(StatsBehaviour))]
    public class PlayerHealthBehaviour : MonoBehaviour, IDamagable, IHealable
    {
        private StatsBehaviour _stats;

        public event Action OnHealthChanged;

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

            OnHealthChanged?.Invoke();

            if (CurrentHealth <= 0) Destroy(gameObject);
        }

        public void Heal(float healValue)
        {
            CurrentHealth = (CurrentHealth + healValue < MaxHealth) ?
                CurrentHealth + healValue : MaxHealth;

            OnHealthChanged?.Invoke();
        }
    }
}
