using System.Collections.Generic;
using Projectiles;
using Stats;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "passive_item_data", menuName = "Inventory/Passive Item Data", order = 51)]
    public class PassiveItemData : ItemData, IStatModifier, IProjectileBehaviourModifier
    {
        [SerializeField] private List<StatModifier> _statModifiers = new List<StatModifier>();
        [SerializeField] private List<SpellBehaviour> _projectilModifiers = new List<SpellBehaviour>();

        public List<StatModifier> StatModifiers => _statModifiers;
        public List<SpellBehaviour> ProjectileBehaviourModifiers => _projectilModifiers;
    }
}
