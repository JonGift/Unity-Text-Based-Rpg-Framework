using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public string attachedCharacter; // TODO: Change to characterclass
    public GameObject attachedItem; // TODO: Change this to an item.
    public InventoryController inventoryController;
    public string slotType = ""; // Can be empty. If not empty, it's specifically associated with a certain type of item (like a helmet)
    bool slotLocked = false;

    public bool acceptsItems = true;
    public bool acceptsUsable = false;
    public bool acceptsKey = false;
    public bool acceptsEquipment = false;

    public void OnDrop(PointerEventData eventData) {
        if (eventData.pointerDrag != null && attachedItem == null && !slotLocked) {

            if(eventData.pointerDrag.GetComponent<ItemObject>().item.Types.Count > 0 && slotType != "") {
                string type = eventData.pointerDrag.GetComponent<ItemObject>().item.Types[0];
                if (type != slotType)
                    return;

                if (!inventoryController.CheckIfCanLock(eventData.pointerDrag.GetComponent<ItemObject>().item))
                    return;
            }

            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            attachedItem = eventData.pointerDrag.gameObject;
            if (eventData.pointerDrag.GetComponent<DragItem>().itemSlot != null)
                eventData.pointerDrag.GetComponent<DragItem>().itemSlot.GetComponent<ItemSlot>().RemoveAttachedItem();
            eventData.pointerDrag.GetComponent<DragItem>().itemSlot = gameObject;
            if (inventoryController != null && attachedItem != null) {
                inventoryController.SetHighlightedItem(this);
                if(slotType != "")
                    inventoryController.LockSlots(GetItem(), this);
            }
        }
    }

    public void ToggleLocked(bool tf) {
        slotLocked = tf;
        transform.GetChild(1).gameObject.SetActive(tf);
    }

    public bool GetLocked() {
        return slotLocked;
    }

    public void RemoveAttachedItem() {
        if (slotType != "")
            inventoryController.UnlockSlots(GetItem(), this);
        attachedItem = null;
    }

    public bool hasItem() {
        return attachedItem != null;
    }

    // get item
    public Item GetItem() {
        if (attachedItem)
            return attachedItem.GetComponent<ItemObject>().item;
        else
            return null;
    }

}
