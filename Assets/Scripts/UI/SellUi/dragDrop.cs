using GameData;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject carrotPrefab;
    [SerializeField] private string foodName; // "C", "Cb", or "T"

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    private Transform originalParent;
    private bool isOriginal;
    private bool isFromSlot;
    private bool wasPlaced;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = rectTransform.anchoredPosition;
        originalParent = transform.parent;

        isOriginal = transform.parent.name == "Canvas";
        isFromSlot = !isOriginal && transform.parent.name == "CarrotSlots";
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isOriginal)
        {
            if (!CanDrag()) return;

            GameObject clone = Instantiate(carrotPrefab, canvas.transform);
            clone.GetComponent<RectTransform>().anchoredPosition = rectTransform.anchoredPosition;

            DragDrop cloneScript = clone.GetComponent<DragDrop>();
            cloneScript.isOriginal = false;
            cloneScript.SetFromSlot(false);

            eventData.pointerDrag = clone;
            ExecuteEvents.Execute(clone, eventData, ExecuteEvents.beginDragHandler);
            return;
        }

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;

        if (isFromSlot)
        {
            originalPosition = rectTransform.anchoredPosition;
            originalParent = transform.parent;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!CanDrag()) return;
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(!CanDrag())return;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        GameObject dropTarget = eventData.pointerCurrentRaycast.gameObject;
        bool validDrop = dropTarget != null &&
                         (dropTarget.GetComponent<itemSlot>() || dropTarget.GetComponent<RefundSlot>());

        if (validDrop)
        {
            // Considered placed in UI
            wasPlaced = true;
            ReduceFood();
        }
        else
        {
            if (isFromSlot)
            {
                // Return to original slot
                transform.SetParent(originalParent);
                rectTransform.anchoredPosition = originalPosition;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }


    public void OnPointerDown(PointerEventData eventData) { }

    public bool IsFromSlot() => isFromSlot;
    public void SetFromSlot(bool value) => isFromSlot = value;

    public bool CanDrag()
    {
        return foodName switch
        {
            "C" => GameDataManager.getCarrotCount() > 0,
            "Cb" => GameDataManager.getCabbageCount() > 0,
            "T" => GameDataManager.getTomatoCount() > 0,
            _ => false
        };
    }

    private void ReduceFood()
    {
        switch (foodName)
        {
            case "C": GameDataManager.ReduceCarrot(); break;
            case "Cb": GameDataManager.ReduceCabbage(); break;
            case "T": GameDataManager.ReduceTomato(); break;
        }
    }

    private void RestoreFood()
    {
        switch (foodName)
        {
            case "C": GameDataManager.AddCarrot(); break;
            case "Cb": GameDataManager.AddCabbage(); break;
            case "T": GameDataManager.AddTomato(); break;
        }
    }
}
