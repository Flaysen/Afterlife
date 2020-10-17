using System;
using System.Collections.Generic;

namespace Stats
{
    public class StatData
    {
        private readonly List<StatModifier> _statModifiers = new List<StatModifier>();

        private readonly float _baseValue = 0f;
        private bool _isDirty = true;

        private float _value;

        public float Value
        {
            get
            {
                if (_isDirty)
                {
                    _value = CalculateValue();
                    _isDirty = false;
                }

                return _value;
            }
        }

        public StatData(float initialValue) => _baseValue = initialValue;

        public StatData(Stat statType) => _baseValue = statType.InitialValue;

        public void AddModifier(StatModifier statModifier)
        {
            _isDirty = true;
            int index = _statModifiers.BinarySearch(statModifier, new ByPriority());
            if (index < 0)
                index = ~index;
            _statModifiers.Insert(index, statModifier);

        }

        public void RemoveModifier(StatModifier statModifier)
        {
            _isDirty = true;
            _statModifiers.Remove(statModifier);
        }

        protected virtual float CalculateValue()
        {
            float finalValue = _baseValue;
            float sumPercentAdd = 0;

            for (int i = 0; i < _statModifiers.Count; i++)
            {
                StatModifier mod = _statModifiers[i];

                if (mod.StatModeType == StatModType.Flat)
                {
                    finalValue += mod.Value;
                }
                else if (mod.StatModeType == StatModType.PercentAdd)
                {
                    sumPercentAdd += mod.Value;

                    if (i + 1 >= _statModifiers.Count || _statModifiers[i + 1].StatModeType != StatModType.PercentAdd)
                    {
                        finalValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                }
                else if (mod.StatModeType == StatModType.PercentMult)
                {
                    finalValue *= 1 + mod.Value;
                }
            }

            return (float)Math.Round(finalValue, 4);
        }

        private class ByPriority : IComparer<StatModifier>
        {
            public int Compare(StatModifier a, StatModifier b)
            {
                if (a.StatModeType > b.StatModeType) return 1;
                else if (a.StatModeType < b.StatModeType) return -1;
                else return 0;
            }
        }
    }
}

