using UnityEngine;
using UnityEngine.EventSystems;

public class RefundSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("Carrot"))
        {
            DragDrop dragDrop = eventData.pointerDrag.GetComponent<DragDrop>();
            
            // Only allow refunding of carrots from slots
            if (dragDrop.isFromSlott())
            {
                // Add your refund logic here (currency, etc.)
                Debug.Log("Carrot refunded!");
                
                // Destroy the carrot
                Destroy(eventData.pointerDrag.gameObject);
            }
        }
    }
}