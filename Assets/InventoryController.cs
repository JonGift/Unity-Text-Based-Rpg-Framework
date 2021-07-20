using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public Text text;
    List<ItemSlot> itemSlots;
    List<ItemSlot> equipmentSlots;

    public ItemSlot highlightedItemSlot;

    // Start is called before the first frame update
    void Awake()
    {
        text = transform.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetComponent<Text>();
        itemSlots = new List<ItemSlot>();
        equipmentSlots = new List<ItemSlot>();
        foreach (ItemSlot item in transform.GetChild(2).GetChild(0).GetComponentsInChildren<ItemSlot>()) {
            itemSlots.Add(item);
            item.inventoryController = this;
        }
        foreach (ItemSlot item in transform.GetChild(2).GetChild(3).GetComponentsInChildren<ItemSlot>()) {
            equipmentSlots.Add(item);
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
        Destroy(highlightedItemSlot.attachedItem.gameObject);
        text.text = "";
        highlightedItemSlot = null;
    }

    public List<Item> GetEquipment() {
        List<Item> items = new List<Item>();
        foreach (ItemSlot slot in equipmentSlots) {
            Item item = slot.GetItem();
            if (item != null)
                items.Add(item);
        }

        Debug.Log(items);
        return items;
    }

    public void LockSlots(Item item, ItemSlot originalSlot) {
        foreach(string type in item.Types)
            foreach(ItemSlot slot in equipmentSlots) {
                if (slot == originalSlot)
                    continue;
                if (slot.slotType == type)
                    slot.ToggleLocked(true);
            }
    }
    public void UnlockSlots(Item item, ItemSlot originalSlot) {
        foreach (string type in item.Types)
            foreach (ItemSlot slot in equipmentSlots) {
                if (slot == originalSlot)
                    continue;
                if (slot.slotType == type)
                    slot.ToggleLocked(false);
            }
    }

    public bool CheckIfCanLock(Item item) {
        foreach (string type in item.Types)
            foreach (ItemSlot slot in equipmentSlots) 
                if (slot.slotType == type && (slot.GetLocked() || slot.attachedItem != null))
                    return false;

        return true;
    }
}
