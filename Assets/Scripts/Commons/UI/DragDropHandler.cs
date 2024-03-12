using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropHandler : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private Transform parentOnDragEnd;
    private Item item;
    private InventorySlot slot;
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 startPosition;
    private bool changePos = true;
    private SpecialInventorySlot specialSlot;
    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag ");
        rectTransform.anchoredPosition += eventData.delta / (canvas.scaleFactor * parentOnDragEnd.localScale);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Drag start");
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        startPosition = rectTransform.position;
        changePos = true;
        parentOnDragEnd = this.transform.parent;
        transform.SetParent(canvas.transform);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag end");


        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        if (changePos) rectTransform.position = startPosition;
        transform.SetParent(parentOnDragEnd);

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer down");
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop");

    }
    public void SetSpecialSlot(SpecialInventorySlot specialInventorySlot)
    {
        specialSlot = specialInventorySlot;
        RefreshItem(false);
    }
    public void SetSlot(InventorySlot slot)
    {
        this.slot = slot;
        RefreshItem(true);
    }
    public void RefreshItem(bool isSlot)
    {
        if (isSlot) item = slot.GetItem();
        else item = specialSlot.GetItem();
    }
    public Item GetDropItem()
    {
        return item;
    }
    public void DoNotChangePos()
    {
        changePos = false;
    }
    public void SetSlotAsDesAct()
    {
        if (slot != null) slot.DesActive();
        else specialSlot.DesActive();
    }
}
