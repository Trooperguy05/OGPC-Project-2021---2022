using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    // list of items in the game \\
    public enum ItemType {
        Coin,
        HealthPotion,
        ManaPotion,
    }

    // itme properties \\
    public ItemType itemType;
    public int quantity;
    public bool stackable;
    private bool consumable;

    // item constructor
    public Item(ItemType type, int amount, bool stack, bool consume) {
        itemType = type;
        quantity = amount;
        stackable = stack;
        consumable = consume;
    }

    // method that returns whether the item can be consumed \\
    public bool isConsumable() {
        return consumable;
    }
}
