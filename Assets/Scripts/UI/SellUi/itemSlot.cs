using UnityEngine;
using UnityEngine.EventSystems;
using GameData;

public class itemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null )
        {
            // Only accept carrots that aren't already in a slot
            DragDrop dragDrop = eventData.pointerDrag.GetComponent<DragDrop>();
            if (!dragDrop.IsFromSlot())
            {
                if(!dragDrop.CanDrag())return;
                eventData.pointerDrag.transform.SetParent(transform);
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                dragDrop.SetFromSlot(true); // Set the status to indicate it's now in a slot
                Debug.Log("Dropped on item slot");

                
            }
        }
    }
}