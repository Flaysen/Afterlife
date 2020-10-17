using UnityEngine;

namespace InventorySystem
{
    public abstract class ItemData : ScriptableObject
    {
        [SerializeField] private string _name = "Item name";

        [SerializeField] private string _decription = "Description";

        [SerializeField] private Sprite _image = null;

        public string Name => _name;
        public string Description => _decription;
        public Sprite ItemImage => _image;

        public ItemType Type;
    }
}