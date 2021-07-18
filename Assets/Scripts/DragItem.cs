using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    private RectTransform dragRectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    public GameObject itemSlot;

    private void Awake() {
        dragRectTransform = GetComponent<RectTransform>();
        canvas = FindObjectOfType<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (itemSlot != null)
            itemSlot.GetComponent<ItemSlot>().inventoryController.SetHighlightedItem(itemSlot.GetComponent<ItemSlot>());
        //transform.GetChild(1).gameObject.SetActive(true);
    }

    public void OnBeginDrag(PointerEventData eventData) {
        canvasGroup.alpha = .65f;
        canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData) {
        dragRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        if (itemSlot) {
            GetComponent<RectTransform>().anchoredPosition = itemSlot.GetComponent<RectTransform>().anchoredPosition;
            itemSlot.GetComponent<ItemSlot>().attachedItem = gameObject;
        }
    }

}

