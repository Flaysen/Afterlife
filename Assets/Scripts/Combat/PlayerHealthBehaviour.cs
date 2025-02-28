﻿using System;
using UnityEngine;
using Stats;
using Combat;
using Core;

namespace Resource
{
    [RequireComponent(typeof(StatsBehaviour))]
    [RequireComponent(typeof(DamageHighlight))]
    public class PlayerHealthBehaviour : MonoBehaviour, IDamagable, IHealable
    {
        public static event Action OnPlayerDeath;

        [SerializeField] private GameObject _deathParticles;
        [SerializeField] private float _immunityTime;
        private StatsBehaviour _stats;
        private DamageHighlight _damageHighlight;
        private float _actualTime;
        public event Action OnHealthChanged;
   
        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; private set; }
        private void Start()
        {
            _stats = GetComponent<StatsBehaviour>();
            _damageHighlight = GetComponent<DamageHighlight>();
            MaxHealth = _stats.GetStatValue(StatType.Health);
            CurrentHealth = MaxHealth;
        }
        public void TakeDamage(float damage)
        {
            if(Time.time > _actualTime + _immunityTime)
            {
                 CurrentHealth = (CurrentHealth - damage > 0) ?
                 CurrentHealth - damage : 0;
                OnHealthChanged?.Invoke();
                _damageHighlight.HighLight();
                _actualTime = Time.time;

                if (CurrentHealth <= 0)
                {
                    GameObject deathParticles = Instantiate(_deathParticles,
                    transform.position + new Vector3(0, 0.5f, 0),
                    Quaternion.identity);
                    deathParticles.GetComponent<DestroyAfterTime>().StartTimer(0.5f);
                    OnPlayerDeath?.Invoke();
                }
            }          
        }
        public void Heal(float healValue)
        {
            CurrentHealth = (CurrentHealth + healValue < MaxHealth) ?
                CurrentHealth + healValue : MaxHealth;
            OnHealthChanged?.Invoke();
        }
    }
}
