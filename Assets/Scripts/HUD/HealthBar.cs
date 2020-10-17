using UnityEngine;
using System;
using Resource;

namespace HUD
{
    public class HealthBar : MonoBehaviour
    {
        public event Action<Vector3> OnSizeAdjusted;

        private PlayerHealthBehaviour _health;

        private float _initialSize;

        private void Awake()
        {
            _health = FindObjectOfType<PlayerHealthBehaviour>();

            _health.OnHealthChanged += AdjustSize;

            _initialSize = transform.localScale.x;
        }

        private void AdjustSize()
        {
            float size = _initialSize * (_health.CurrentHealth / _health.MaxHealth);

            Vector3 adjustedScale = new Vector3(size, transform.localScale.y, transform.localScale.z);

            transform.localScale = adjustedScale;

            OnSizeAdjusted?.Invoke(adjustedScale);
        }
    }
}
