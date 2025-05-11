using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject carrotPrefab; // Reference to the original carrot prefab
    
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    private Transform originalParent;
    private bool isOriginal;
    private bool isFromSlot;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = rectTransform.anchoredPosition;
        originalParent = transform.parent;
        
        // Automatically detect if this is the original carrot
        isOriginal = gameObject.name == "DragableCarrot" && transform.parent.name == "Canvas";
        isFromSlot = !isOriginal && transform.parent.name == "CarrotSlots";
    }

    public bool isFromSlott(){
        return isFromSlot;
    }

    public void setFromSlot(bool slotStatus){
        this.isFromSlot = slotStatus;   
    }

    public void OnBeginDrag(PointerEventData eventData) {
        // If this is the original carrot, create a clone to drag
        if (isOriginal) {
            GameObject clone = Instantiate(carrotPrefab, originalParent);
            clone.GetComponent<RectTransform>().anchoredPosition = originalPosition;
            clone.GetComponent<DragDrop>().isOriginal = false;
            
            // Start dragging the clone instead
            eventData.pointerDrag = clone;
            clone.GetComponent<DragDrop>().OnBeginDrag(eventData);
            return;
        }
        
        // For all other carrots (clones and slot carrots)
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        
        // If coming from slot, remember original slot position
        if (isFromSlot) {
            originalPosition = rectTransform.anchoredPosition;
            originalParent = transform.parent;
        }
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        
        // Check if dropped on valid target
        GameObject dropTarget = eventData.pointerCurrentRaycast.gameObject;
        
        // If dropped on nothing or invalid target
        if (dropTarget == null || 
            (!dropTarget.GetComponent<itemSlot>() && !dropTarget.GetComponent<RefundSlot>())) 
        {
            if (isFromSlot) {
                // Return to original slot
                transform.SetParent(originalParent);
                rectTransform.anchoredPosition = originalPosition;
            } else {
                // Destroy non-slot carrots dropped in invalid locations
                Destroy(gameObject);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        // Optional: Add any click effects here
    }
}