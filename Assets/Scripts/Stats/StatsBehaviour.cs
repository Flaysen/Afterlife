using System.Collections.Generic;
using UnityEngine;
using Projectiles;

namespace Stats
{
    public class StatsBehaviour : MonoBehaviour
    {
        [SerializeField] private BaseStats _baseStats;
        private Dictionary<Stat, StatData> Stats = new Dictionary<Stat, StatData>();

        public List<SpellBehaviour> ProjectileModifiers = new List<SpellBehaviour>();

        public BaseStats BaseStats => _baseStats;

        private void Awake()
        {
            InitializeStatsDirectory();
        }

        private void InitializeStatsDirectory()
        {            
            foreach (var stat in _baseStats.Stats)
            {
                Stats.Add(stat.StatType, new StatData(stat.Value));
            }
        }        

        public float GetStatValue(StatType statType)
        {
            foreach (KeyValuePair<Stat, StatData> stat in Stats)
            {
                if (statType == stat.Key.StatType)
                {
                    return stat.Value.Value;                    
                }
            }
            return 0;
        }

        public void RemoveStatMod(IStatModifier modifier)
        {
            foreach (StatModifier mod in modifier.StatModifiers)
            {
                foreach (KeyValuePair<Stat, StatData> stat in Stats)
                {
                    if (mod.StatType == stat.Key)
                    {
                        stat.Value.RemoveModifier(mod);
                    }
                }
            }
        }

        public void AddStatMod(IStatModifier modifier)
        {          
            foreach (StatModifier mod in modifier.StatModifiers)
            {
                foreach(KeyValuePair<Stat, StatData> stat in Stats)
                {
                    if(mod.StatType == stat.Key)
                    {
                        stat.Value.AddModifier(mod);
                    }
                }
            }
        }

        public void AddProjectileMod(IProjectileBehaviourModifier modifier)
        {
            if(modifier.ProjectileBehaviourModifiers != null)
            {
                foreach(SpellBehaviour mod in modifier.ProjectileBehaviourModifiers)
                {
                    ProjectileModifiers.Add(mod);                        
                }         
            }
        }

        public void RemoveProjectileMod(IProjectileBehaviourModifier modifier)
        {
           Debug.Log("Remove"); 
        }

    }
}
