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
    private InventoryController inventoryController;

    int spriteOrder;

    private void Awake() {
        dragRectTransform = GetComponent<RectTransform>();
        canvas = FindObjectOfType<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        spriteOrder = transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder;
        inventoryController = FindObjectOfType<InventoryController>();
        if(inventoryController == null) // is in loot window
            inventoryController = transform.parent.parent.GetChild(0).GetComponent<InventoryController>();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Right) {
            inventoryController.SetItemParent(this.gameObject);
            if(GetComponent<ItemObject>().itemType == "")
                inventoryController.GetNextOpenItemSlot().ForcePickUpItem(this.gameObject);
            else if (GetComponent<ItemObject>().itemType == "Use")
                inventoryController.GetNextOpenUseSlot().ForcePickUpItem(this.gameObject);
            else if (GetComponent<ItemObject>().itemType == "Key")
                inventoryController.GetNextOpenKeySlot().ForcePickUpItem(this.gameObject);
            else
                inventoryController.GetNextOpenEquipSlot().ForcePickUpItem(this.gameObject);
        }
        if (itemSlot != null)
            if(itemSlot.GetComponent<ItemSlot>().inventoryController != null)
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
            if (itemSlot.GetComponent<ItemSlot>().CheckIfCanAttach(gameObject)) {
                if(itemSlot.GetComponent<ItemSlot>().inventoryController != null)
                    inventoryController.SetItemParent(this.gameObject);
                GetComponent<RectTransform>().anchoredPosition = itemSlot.GetComponent<RectTransform>().anchoredPosition;
            }
        }
    }

}

