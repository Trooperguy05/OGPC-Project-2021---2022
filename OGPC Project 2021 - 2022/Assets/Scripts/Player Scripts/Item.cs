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
        Sword,
        Boulder,
        Amulet,
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

    // method that returns name of item \\
    public string getName() {
        if (itemType == ItemType.Coin) {
            return "coin";
        }
        else if (itemType == ItemType.HealthPotion) {
            return "health potion";
        }
        else if (itemType == ItemType.ManaPotion) {
            return "mana potion";
        }
        else if (itemType == ItemType.Sword) {
            return "sword";
        }
        else if (itemType == ItemType.Boulder) {
            return "boudler";
        }
        else if (itemType == ItemType.Amulet) {
            return "amulet";
        }
        return "";
    }
}
