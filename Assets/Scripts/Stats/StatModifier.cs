using System;
using UnityEngine;

namespace Stats
{
    public enum StatModType
    {
        Flat,
        PercentAdd,
        PercentMult,
    }
    
    [Serializable]
    public class StatModifier
    {
        [SerializeField] private Stat _statType;

        [SerializeField] private StatModType _statModType;

        [SerializeField] private float _value;
        private object _source;
        public StatModifier(object source, Stat statType, StatModType statModeType, float value)
        {
            _statType = statType;
            _statModType = statModeType;
            _value = value;
        }
        public Stat StatType => _statType;
        public StatModType StatModeType => _statModType;
        public float Value => _value;
    }
}

