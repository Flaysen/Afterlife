using System.Collections.Generic;
using UnityEngine;

namespace Stats
{
    [CreateAssetMenu(fileName = "base_stats", menuName = "Stats/Base Stats", order = 52)]
    public class BaseStats : ScriptableObject
    {
        [SerializeField] private List<BaseStat> _stats = new List<BaseStat>();

        public List<BaseStat> Stats => _stats;

        [System.Serializable]
        public class BaseStat
        {
            [SerializeField] private Stat _statType = null;

            [SerializeField] private float _value = 0f;

            public Stat StatType => _statType;
            public float Value => _value;
        }
    }
}
