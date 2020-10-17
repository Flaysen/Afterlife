using System.Collections.Generic;
using UnityEngine;
using InventorySystem;

namespace HUD
{
    public class InventoryItemsPanel : MonoBehaviour
    {
        [SerializeField] private ItemType _itemsType;
        
        private List<InventorySlot> _slots = new List<InventorySlot>();

        private Inventory _inventory;

        void Awake()
        {
            _inventory = FindObjectOfType<Inventory>();

            _inventory.ItemAdded += AddItemToPanel;

            InitialzieSlots();
        }

        private void InitialzieSlots()
        {
            InventorySlot [] slots = GetComponentsInChildren<InventorySlot>();

            foreach(InventorySlot slot in slots)
            {
                _slots.Add(slot);    
            }
        }

        private void AddItemToPanel(Item item)
        {
            if(item.Data.Type == _itemsType)
            {
                foreach(InventorySlot slot in _slots)
                {
                    if(slot.IsLocked == false)
                    {

                        slot.Image.enabled = true;

                        ItemDragHandler ItemDragHandler = slot.GetComponent<ItemDragHandler>();

                        ItemDragHandler.DragedItem = item;
                        
                        slot.Image.sprite = item.Data.ItemImage;

                        slot.IsLocked = true;
                        
                        return;
                    }
                }
            }           
        }
    }
}