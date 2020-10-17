using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpellSystem;

namespace InventorySystem
{
    public class SpellItem : Item
    {
        private SpellManager _spellManager;

        void Awake()
        {
            _spellManager = FindObjectOfType<SpellManager>();

            _data.Type = ItemType.Spell;
        }

        public override void GetItemBehaviour()
        {
            //_spellManager.AddSpell((ISpellProvider)_data);
        }
    }

}

