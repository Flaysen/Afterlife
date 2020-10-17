using System.Collections;
using System.Collections.Generic;
using SpellSystem;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "spell_item_data", menuName = "Inventory/Spell Item Data", order = 53)]
    public class SpellItemData : ItemData, ISpellsProvider
    {
        [SerializeField] private List<SpellData> _spells = new List<SpellData>();

        public List<SpellData> Spells => _spells; 
    } 
}

