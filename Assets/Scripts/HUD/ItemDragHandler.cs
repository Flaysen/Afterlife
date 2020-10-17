using InventorySystem;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item DragedItem { get; set; } 

    private Vector3 _startPositon;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPositon = transform.position;
        eventData.pointerDrag.GetComponent<RectTransform>().SetAsLastSibling();

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = _startPositon;
    }
}
