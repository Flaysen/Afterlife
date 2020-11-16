using System;
using System.Collections.Generic;
using Stats;
using Combat;
using UnityEngine;
using Projectiles;

namespace InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        private List<Item> _items = new List<Item>();

        //private Dictionary<ItemType,Item> _items = new Dictionary<ItemType, Item>();
        
        public Action<Item> ItemAdded;

        public Action<Item> ItemRemoved;

        private StatsBehaviour _stats;

        private AttackHandler _attackHandler;

        private void Awake()
        {
            _attackHandler = GetComponent<AttackHandler>();
            
            _stats = GetComponent<StatsBehaviour>();
            
            Item.OnPickUp += AddItem;            
        }

        public void AddItem(Item item)
        {         
            _items.Add(item);

            item.GetItemBehaviour();

            ItemAdded?.Invoke(item);        
        }

        public void RemoveItem(Item item)
        {
            if (_items.Contains(item))
            {
                _items.Remove(item);
            }
        }
    }
}

