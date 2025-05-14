using UnityEngine;
using UnityEngine.EventSystems;
using GameData;

public class RefundSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            DragDrop dragDrop = eventData.pointerDrag.GetComponent<DragDrop>();

            // Only allow refunding of carrots from slots
            if (dragDrop.IsFromSlot())
            {
                // Add your refund logic here (currency, etc.)
                if (eventData.pointerDrag.GetComponent<DragDrop>().name == "DragableCarrot(Clone)")
                {
                    GameDataManager.AddCarrot();
                }
                else if (eventData.pointerDrag.GetComponent<DragDrop>().name == "DragableCabbage(Clone)")
                {
                    GameDataManager.AddCabbage();
                }
                else if (eventData.pointerDrag.GetComponent<DragDrop>().name == "DragableTomato(Clone)")
                {
                    GameDataManager.AddTomato();
                }

                // Destroy the carrot
                Destroy(eventData.pointerDrag.gameObject);
            }
        }
    }
}