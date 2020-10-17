using System;
using InventorySystem;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, ISlot
{
    private Image _image;

    private ItemDragHandler _itemDragHandler;

    public Image Image => _image;
    
    public bool IsLocked { get; set; }
    public int Id { get; set; }

    public static event Action<Item> OnItemClicked;

    void Awake()
    {
        _image = GetComponent<Image>();

        _itemDragHandler = GetComponent<ItemDragHandler>();

    }

    public void ItemClicked()
    {
        OnItemClicked?.Invoke(_itemDragHandler.DragedItem);
    }
}
