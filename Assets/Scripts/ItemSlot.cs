using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public string attachedCharacter; // TODO: Change to characterclass
    private Vector2 centerPos;
    public GameObject attachedItem; // TODO: Change this to an item.

    public bool acceptsItems = true;
    public bool acceptsUsable = false;
    public bool acceptsKey = false;
    public bool acceptsEquipment = false;


    // Start is called before the first frame update
    void Awake()
    {
        centerPos = transform.position;
    }

    public void OnDrop(PointerEventData eventData) {
        if (eventData.pointerDrag != null) {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            attachedItem = eventData.pointerDrag.gameObject;
            if (eventData.pointerDrag.GetComponent<DragItem>().itemSlot != null)
                eventData.pointerDrag.GetComponent<DragItem>().itemSlot.GetComponent<ItemSlot>().attachedItem = null;
            eventData.pointerDrag.GetComponent<DragItem>().itemSlot = gameObject;
        }
    }

    public bool hasItem() {
        return attachedItem != null;
    }

    // get item


}
