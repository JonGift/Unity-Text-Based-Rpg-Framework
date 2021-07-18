using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public Text text;
    List<ItemSlot> itemSlots;

    public ItemSlot highlightedItemSlot;

    // Start is called before the first frame update
    void Awake()
    {
        text = transform.GetChild(2).GetChild(4).GetChild(0).GetChild(0).GetComponent<Text>();
        itemSlots = new List<ItemSlot>();
        foreach (ItemSlot item in transform.GetChild(2).GetChild(0).GetComponentsInChildren<ItemSlot>()) {
            itemSlots.Add(item);
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
}
