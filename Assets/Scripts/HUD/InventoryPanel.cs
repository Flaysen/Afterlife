using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using InventorySystem;

namespace HUD
{
    public class InventoryPanel : MonoBehaviour
    {
        [SerializeField] private Image _itemPreviewImage;

        [SerializeField] private TextMeshProUGUI _itemCaptionLabel;

        [SerializeField] private TextMeshProUGUI _itemDescription;

        private CanvasGroup _canvas;

        private bool _isOpen;

        void Awake()
        {
            _isOpen = false; 

            _canvas = GetComponent<CanvasGroup>();

            InventorySlot.OnItemClicked += DisplayItemData;
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.I))
            {        
                HandleInventoryDisplay();
            }
        }

        private void HandleInventoryDisplay()
        {   
            _isOpen = !_isOpen;
            
            _canvas.alpha = _isOpen ? 1 : 0;

            _canvas.interactable = _isOpen ? true : false;

             _canvas.blocksRaycasts = _isOpen ? true : false;
        }

        public void DisplayItemData(Item item)
        {
            _itemPreviewImage.sprite = item.Data.ItemImage;
            
            _itemCaptionLabel.text = item.Data.Name;

            _itemDescription.text = item.Data.Description;
        }

    }
}

