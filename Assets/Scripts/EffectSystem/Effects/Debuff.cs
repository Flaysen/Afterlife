using UnityEngine;
using Stats;
using System.Collections.Generic;

namespace Effects 
{
    [CreateAssetMenu(fileName = "debuff", menuName = "Effects/Debuff", order = 55)]
    public class Debuff : Effect, IStatModifier
    {
        [SerializeField] private List<StatModifier> _statModifiers = new List<StatModifier>();
        public List<StatModifier> StatModifiers => _statModifiers;
        private StatsBehaviour _stats;

        public override void BeginEffect(Transform target)
        {
            _stats = target.GetComponent<StatsBehaviour>();

            if(_stats)
            {    
                _stats.AddStatMod(this);          
            }
        }
        public override void EndEffect(Transform target)
        {
        if(_stats)
            {          
                _stats.RemoveStatMod(this);     
            }
        }  
    }
}


