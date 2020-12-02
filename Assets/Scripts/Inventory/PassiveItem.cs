using Stats;
using Projectiles;

namespace InventorySystem
{
    public class PassiveItem : Item
    {
        private StatsBehaviour _stats;
        void Awake()
        {
            _stats = FindObjectOfType<StatsBehaviour>();

            _data.Type = ItemType.Passive;
        }
        public override void GetItemBehaviour()
        {      
            _stats.AddStatMod((IStatModifier)_data);

            _stats.AddProjectileMod((IProjectileBehaviourModifier)_data);     
        }
    }
}
