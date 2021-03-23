using System.Collections;
using System.Collections.Generic;
using Core;
using TMPro;
using UnityEngine;

namespace InventorySystem
{
    public class ItemFloatingText : MonoBehaviour
    {
        private TMP_Text _text;
        private IInteractable _interactable;
        [SerializeField] private GameObject _chest;

        private void Awake()
        {
            _interactable = (_chest) ? _chest.GetComponent<IInteractable>() : 
            GetComponentInParent<Item>();

            _text = GetComponent<TMP_Text>();       

            if(_interactable != null && _interactable.GetType() == typeof(PassiveItem))
            {
                Item item = (Item)_interactable;
                _text.SetText(item.Data.Name);
            }   
            _interactable.OnInteractDisplay += (isVisible) => { _text.enabled = isVisible; };
        }
    }
}


