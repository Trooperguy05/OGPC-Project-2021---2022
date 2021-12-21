using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    // inventory list
    public List<Item> playerInventory = new List<Item>();

    public InventoryData(Inventory inv) {
        // adds the items in the player inventory into the data
        for (int i = 0; i < inv.inventory.Count; i++) {
            playerInventory.Add(inv.inventory[i]);
        }
    }
}
