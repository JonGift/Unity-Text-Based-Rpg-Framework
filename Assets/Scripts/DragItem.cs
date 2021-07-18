using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
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

