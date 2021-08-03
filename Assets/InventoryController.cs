using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public Text text;
    List<ItemSlot> itemSlots;
    List<ItemSlot> equipmentSlots;

    List<ItemSlot> equippedItemSlots;

    public ItemSlot highlightedItemSlot;

    // Start is called before the first frame update
    void Awake()
    {
        text = transform.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetComponent<Text>();
        itemSlots = new List<ItemSlot>();
        equipmentSlots = new List<ItemSlot>();
        equippedItemSlots = new List<ItemSlot>();
        foreach (ItemSlot item in transform.GetChild(2).GetChild(0).GetComponentsInChildren<ItemSlot>()) {
            itemSlots.Add(item);
            item.inventoryController = this;
        }

        // TODO: These get moved to the character screen.
        foreach (ItemSlot item in transform.GetChild(2).GetChild(3).GetComponentsInChildren<ItemSlot>()) {
            if(item.slotType == "") {
                equipmentSlots.Add(item);
            } else {
                equippedItemSlots.Add(item);
            }

            item.inventoryController = this;
        }


    }

    public void SetHighlightedItem(ItemSlot itemSlot) {
        if(highlightedItemSlot)
            if(highlightedItemSlot.attachedItem)
                highlightedItemSlot.attachedItem.transform.GetChild(1).gameObject.SetActive(false);
        highlightedItemSlot = itemSlot;
        highlightedItemSlot.attachedItem.transform.GetChild(1).gameObject.SetActive(true);
        text.text = itemSlot.attachedItem.GetComponent<ItemObject>().item.Description;
        transform.GetChild(2).GetChild(5).GetChild(1).GetComponent<Button>().interactable = true;
    }

    public void DiscardItem() {
        UnlockSlots(highlightedItemSlot.attachedItem.GetComponent<ItemObject>().item, highlightedItemSlot);
        Destroy(highlightedItemSlot.attachedItem.gameObject);
        text.text = "";
        highlightedItemSlot = null;
    }

    public List<Item> GetEquipment() {
        List<Item> items = new List<Item>();
        foreach (ItemSlot slot in equippedItemSlots) {
            Item item = slot.GetItem();
            if (item != null)
                items.Add(item);
        }

        return items;
    }

    public void SetItemParent(GameObject itemObj) {
        if(itemObj.GetComponent<ItemObject>().itemType == "") {
            itemObj.transform.SetParent(transform.GetChild(2).GetChild(0));
        }
        else if (itemObj.GetComponent<ItemObject>().itemType == "Use") {
            itemObj.transform.SetParent(transform.GetChild(2).GetChild(1));
        }
        else if (itemObj.GetComponent<ItemObject>().itemType == "Key") {
            itemObj.transform.SetParent(transform.GetChild(2).GetChild(2));
        }
        else {
            itemObj.transform.SetParent(transform.GetChild(2).GetChild(3));
        }
    }

    public ItemSlot GetNextOpenItemSlot() {
        foreach(ItemSlot slot in itemSlots) {
            Item item = slot.GetItem();
            if (item == null)
                return slot;
        }

        return null;
    }

    public ItemSlot GetNextOpenUseSlot() {
        foreach (ItemSlot slot in itemSlots) {
            Item item = slot.GetItem();
            if (item == null)
                return slot;
        }

        return null;
    }

    public ItemSlot GetNextOpenKeySlot() {
        foreach (ItemSlot slot in itemSlots) {
            Item item = slot.GetItem();
            if (item == null)
                return slot;
        }

        return null;
    }
    public ItemSlot GetNextOpenEquipSlot() {
        foreach (ItemSlot slot in equipmentSlots) {
            Item item = slot.GetItem();
            if (item == null)
                return slot;
        }

        return null;
    }

    public void LockSlots(Item item, ItemSlot originalSlot) {
        foreach(string type in item.Types)
            foreach(ItemSlot slot in equippedItemSlots) {
                if (slot == originalSlot)
                    continue;
                if (slot.slotType == type)
                    slot.ToggleLocked(true);
            }
    }
    public void UnlockSlots(Item item, ItemSlot originalSlot) {
        foreach (string type in item.Types)
            foreach (ItemSlot slot in equippedItemSlots) {
                if (slot == originalSlot)
                    continue;
                if (slot.slotType == type)
                    slot.ToggleLocked(false);
            }
    }

    public bool CheckIfCanLock(Item item) {
        foreach (string type in item.Types)
            foreach (ItemSlot slot in equippedItemSlots) 
                if (slot.slotType == type && (slot.GetLocked() || slot.attachedItem != null))
                    return false;

        return true;
    }
}
