using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemObject : MonoBehaviour
{
    public string itemId; // When instantiated, this object finds this item from the items list.
    public Item item;
    public bool temp;
    public string itemType = "";
    Text text;
    public List<string> itemTypes;

    private void Awake() {
        text = transform.GetChild(0).GetChild(1).GetComponent<Text>();
        item = new Item(Items.normalRock);
        if (temp)
            item = new Item(Items.specialRock);

        if(item.MaxStack > 1) {
            item.Stack = Random.Range(5, 90);
            text.text = item.Stack.ToString();
            text.transform.parent.gameObject.SetActive(true);
        }

        if (itemTypes.Count > 0)
            item.Types = itemTypes;

        if (item.Types.Count > 0)
            itemType = item.Types[0];
    }

}
