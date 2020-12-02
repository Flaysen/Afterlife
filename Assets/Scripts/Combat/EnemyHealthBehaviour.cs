using UnityEngine;
using Stats;
using Resource;
using System;
using Core;

namespace Combat
{
    [RequireComponent(typeof(StatsBehaviour))]
    [RequireComponent(typeof(DamageHighlight))]
    public class EnemyHealthBehaviour : MonoBehaviour, IDamagable, IHealable
    {
        [SerializeField] private GameObject _deathParticles;
        [SerializeField] private DamageHighlight _damageHighlight;
        public static event Action<float> OnDamageTaken;
        public static event Action<EnemyHealthBehaviour> OnEnemyDeath;
        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; private set; }
        private StatsBehaviour _stats;

        private void Start()
        {
            _stats = GetComponent<StatsBehaviour>();
            _damageHighlight = GetComponent<DamageHighlight>();

            MaxHealth = _stats.GetStatValue(StatType.Health);
            CurrentHealth = MaxHealth;
        }
        public void TakeDamage(float damage)
        {
            CurrentHealth = (CurrentHealth - damage > 0) ?
                CurrentHealth - damage : 0;

            OnDamageTaken?.Invoke(damage);
            _damageHighlight.HighLight();

            if (CurrentHealth <= 0) 
            {
                OnEnemyDeath?.Invoke(this);

                Destroy(gameObject);
                GameObject deathParticles = Instantiate(_deathParticles,
                    transform.position + new Vector3(0, 0.5f, 0),
                    Quaternion.identity);
                deathParticles.GetComponent<DestroyAfterTime>().StartTimer(0.5f);
            }
        }
        public void Heal(float healValue)
        {
            CurrentHealth = (CurrentHealth + healValue < MaxHealth) ?
                CurrentHealth + healValue : MaxHealth; 
        }
    }
}

