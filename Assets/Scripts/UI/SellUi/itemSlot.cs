using UnityEngine;
using UnityEngine.EventSystems;

public class itemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("Carrot"))
        {
            // Only accept carrots that aren't already in a slot
            DragDrop dragDrop = eventData.pointerDrag.GetComponent<DragDrop>();
            if (!dragDrop.isFromSlott())
            {
                eventData.pointerDrag.transform.SetParent(transform);
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                dragDrop.setFromSlot(true); // Set the status to indicate it's now in a slot
            }
        }
    }
}