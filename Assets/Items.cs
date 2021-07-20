using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items
{
    public static readonly Item normalRock = new Item("normalRock", "Ordinary Rock", "an ordinary rock", "An entirely ordinary rock, with no notable features whatsoever. Why did you pick this up?", "an ordinary-looking rock", 1, 1, 99);
    public static readonly Item specialRock = new Item("specialRock", "Special Rock", "a special rock", "An incredibly rare and special rock. You can tell because of the way it is", "a special-looking rock", 999, 1, 1, "Helmet", "Eyes");

}
