using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace InventorySystem
{
    public class ItemFloatingText : MonoBehaviour
    {
        private TMP_Text _text;
        private Item _item;

        private void Awake()
        {
            _item = GetComponentInParent<Item>();

            _text = GetComponent<TMP_Text>();       

            _text.SetText(_item.Data.Name);  

            _item.OnInteractDisplay += (isVisible) => { _text.enabled = isVisible; };
        }
    }
}


