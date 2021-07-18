using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string Id { get; } // Item identifier. Not displayed in game.
    public string Name { get; set; } // Title of item. Displayed in inventory and such.
    public string DisplayName { get; set; } // How the name is displayed in a sentence
    public string Description { get; set; } // A general description of the item.
    public string ShortDescription { get; set; } // Description for item pick up. You pick up (a worn and broken sword).
    
    public int Value { get; set; }
    public int Weight { get; set; }
    public int MaxStack { get; set; }
    public int Stack { get; set; }
    public Item(string id, string name, string displayName, string description, string shortDescription, int value, int weight, int maxStack) {
        Id = id;
        Name = name;
        DisplayName = displayName;
        Description = description;
        ShortDescription = shortDescription;
        Value = value;
        Weight = weight;
        MaxStack = maxStack;
        Stack = 1;
    }
}
