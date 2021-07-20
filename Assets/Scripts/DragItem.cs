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

    int spriteOrder;

    private void Awake() {
        dragRectTransform = GetComponent<RectTransform>();
        canvas = FindObjectOfType<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        spriteOrder = transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder;
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (itemSlot != null)
            itemSlot.GetComponent<ItemSlot>().inventoryController.SetHighlightedItem(itemSlot.GetComponent<ItemSlot>());
        //transform.GetChild(1).gameObject.SetActive(true);
    }

    public void OnBeginDrag(PointerEventData eventData) {
        canvasGroup.alpha = .65f;
        canvasGroup.blocksRaycasts = false;
        //transform.position = new Vector3(transform.position.x, transform.position.y, -3); // TODO!!
        foreach (SpriteRenderer spr in GetComponentsInChildren<SpriteRenderer>())
            spr.sortingOrder = spriteOrder + 1;
        transform.GetChild(0).GetComponent<Canvas>().sortingOrder = spriteOrder + 1;
    }
    public void OnDrag(PointerEventData eventData) {
        dragRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        //transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        transform.GetChild(0).GetComponent<Canvas>().sortingOrder = spriteOrder;
        foreach (SpriteRenderer spr in GetComponentsInChildren<SpriteRenderer>())
            spr.sortingOrder = spriteOrder;
        if (itemSlot) {
            GetComponent<RectTransform>().anchoredPosition = itemSlot.GetComponent<RectTransform>().anchoredPosition;
            itemSlot.GetComponent<ItemSlot>().attachedItem = gameObject;
        }
    }

}

