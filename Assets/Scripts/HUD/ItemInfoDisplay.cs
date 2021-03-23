using UnityEngine;
using InventorySystem;
using TMPro;

namespace HUD
{
    public class ItemInfoDisplay : MonoBehaviour
    {
        [SerializeField] private float _displayTime;

        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _text.enabled = false;
            Item.OnPickUp += DisplayInfo;
        }

        private void DisplayInfo(Item item)
        {
            _text.SetText(item.Data.Description);
            _text.enabled = true;
            Invoke("HideText", _displayTime);
        }

        private void HideText()
        {
            _text.enabled = false;
        }
    }
}

