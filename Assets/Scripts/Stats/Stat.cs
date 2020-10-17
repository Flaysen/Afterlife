using UnityEngine;

namespace Stats
{
    [CreateAssetMenu(fileName = "stat_type", menuName = "Stats/Stat Type", order = 52)]
    public class Stat : ScriptableObject
    {
        [SerializeField] private string _name = "Stat Type Name";

        [SerializeField] private StatType _statType;

        [SerializeField] private float _initialValue = 0f;

        public string Name => _name;
        public StatType StatType => _statType;
        public float InitialValue => _initialValue;
        
    }
}

