using UnityEngine;
using Core;
using System;

namespace InventorySystem
{
    public abstract class Item : MonoBehaviour, IInteractable
    {
        [SerializeField] protected ItemData _data;
        
        private TriggerOverlap _overlap;     

        public ItemData Data => _data;

        public static event Action<Item> OnPickUp;

        public event Action<bool> OnInteractDisplay;

        public abstract void GetItemBehaviour();

        public void Interact()
        {
            gameObject.SetActive(false);

            OnPickUp?.Invoke(this);
        }

        public void HandleInteractionInfoDisplay(bool isVisible)
        {
            OnInteractDisplay?.Invoke(isVisible);
        }
    }
}

