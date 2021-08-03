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

    public List<string> Types { get; set; }
    
    public int Value { get; set; }
    public int Weight { get; set; }
    public int MaxStack { get; set; }
    public int Stack { get; set; }
    public Item(string id, string name, string displayName, string description, string shortDescription, int value, int weight, int maxStack, params string[] types) {
        Id = id;
        Name = name;
        DisplayName = displayName;
        Description = description;
        ShortDescription = shortDescription;

        Types = new List<string>();
        
        Value = value;
        Weight = weight;
        MaxStack = maxStack;
        Stack = 1;

        foreach (string type in types)
            Types.Add(type);
    }

    public Item(Item templateItem) {
        Id = templateItem.Id;
        Name = templateItem.Name;
        DisplayName = templateItem.DisplayName;
        Description = templateItem.Description;
        ShortDescription = templateItem.ShortDescription;

        Types = new List<string>();

        Value = templateItem.Value;
        Weight = templateItem.Weight;
        MaxStack = templateItem.MaxStack;
        Stack = templateItem.Stack;

        foreach (string type in templateItem.Types)
            Types.Add(type);
    }
}
