using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemObject : MonoBehaviour
{
    public string itemId; // When instantiated, this object finds this item from the items list.
    public Item item;
    public bool temp;
    Text text;

    private void Awake() {
        text = transform.GetChild(0).GetComponent<Text>();
        item = Items.normalRock;
        if (temp)
            item = Items.specialRock;

        if(item.MaxStack > 1) {
            item.Stack = Random.Range(5, 90);
            text.text = item.Stack.ToString();
            text.gameObject.SetActive(true);
        }

    }

}
