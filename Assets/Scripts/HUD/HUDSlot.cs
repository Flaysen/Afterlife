using InventorySystem;
using SpellSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HUDSlot : MonoBehaviour, ISlot, IDropHandler
{
    [SerializeField] private ItemType _itemType;

    private Image _image;

    private CooldownFade _fade;

    public CooldownFade Fade => _fade;

    public Image Image => _image;

    public bool IsLocked { get; set; }

    public int Id { get ; set ; }

    private SpellManager _spellManager;

    void Start()
    
    {
        _image = transform.GetChild(0).GetChild(0).GetComponentInChildren<Image>();

        _fade = GetComponentInChildren<CooldownFade>();

        _spellManager = FindObjectOfType<SpellManager>();

        IsLocked = false;
    }

    public void SetImage(Sprite sprite)
    {
        _image.enabled = true;

        _image.sprite = sprite;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Transform dropedTransform = eventData.pointerDrag.gameObject.GetComponent<Transform>();

        ItemDragHandler dropedDragHandler = dropedTransform.GetComponent<ItemDragHandler>();

        Item dropedItem = dropedDragHandler.DragedItem;

        if(dropedItem != null)
        {
            if (!IsLocked && dropedItem.Data.Type == _itemType)
            {
                _image.enabled = true;
                
                _image.sprite = dropedItem.Data.ItemImage;

                _spellManager.AddSpell((ISpellsProvider)dropedItem.Data, Id);
            }
        }
    }
}
