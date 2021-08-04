using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public string attachedCharacter; // TODO: Change to characterclass
    public GameObject attachedItem; // TODO: Change this to an item.
    public GameObject preAttachedItem;
    public InventoryController inventoryController;
    public string slotType = ""; // Can be empty. If not empty, it's specifically associated with a certain type of item (like a helmet)
    bool slotLocked = false;

    public bool acceptsItems = true;
    public bool acceptsUsable = false;
    public bool acceptsKey = false;
    public bool acceptsEquipment = false;
    public bool lootWindow = false;

    private void Awake() {
        if (preAttachedItem) {
            AttachItem(preAttachedItem);
        }
    }

    public void OnDrop(PointerEventData eventData) {
        if (eventData.pointerDrag != null && attachedItem == null && !slotLocked && !lootWindow) {
            AttachItem(eventData.pointerDrag);
        }
    }


    // TODO: Add in key items and use items
    public bool CheckIfCanAttach(GameObject itemObj) {
        if (itemObj.GetComponent<ItemObject>().itemType == "" && !acceptsItems)
            return false;

        if (itemObj.GetComponent<ItemObject>().itemType != "" && !acceptsEquipment)
            return false;

        return true;
    }
    bool AttachItem(GameObject itemObj) {
        if (!CheckIfCanAttach(itemObj))
            return false;

        if (itemObj.GetComponent<ItemObject>().item.Types.Count > 0 && slotType != "") {
            string type = itemObj.GetComponent<ItemObject>().item.Types[0];
            if (type != slotType)
                return false;

            if (!inventoryController.CheckIfCanLock(itemObj.GetComponent<ItemObject>().item))
                return false;
        }

        itemObj.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        attachedItem = itemObj.gameObject;
        if (itemObj.GetComponent<DragItem>().itemSlot != null)
            itemObj.GetComponent<DragItem>().itemSlot.GetComponent<ItemSlot>().RemoveAttachedItem();
        itemObj.GetComponent<DragItem>().itemSlot = gameObject;
        if (inventoryController != null && attachedItem != null) {
            inventoryController.SetHighlightedItem(this);
            if (slotType != "")
                inventoryController.LockSlots(GetItem(), this);
        }

        return true;
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
        if (preAttachedItem)
            return true;
        if (attachedItem)
            return true;
        return false;
    }

    // This function allows us to place items in an inventory without dragging them (ie right-click)
    public bool ForcePickUpItem(GameObject dragItemObj) {
        // TODO: Need to force parent the item to the correct inventory window.
        if (slotLocked || attachedItem != null)
            return false;

        return AttachItem(dragItemObj);
    }

    // get item
    public Item GetItem() {
        if (attachedItem)
            return attachedItem.GetComponent<ItemObject>().item;
        else
            return null;
    }

}
