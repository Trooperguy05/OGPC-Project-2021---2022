using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // inventory list \\
    public List<Item> inventory = new List<Item>();

    // load the inventory on start up \\
    void Start() {
        loadInventory();
    }

    // testing
    void Update() {
        if (Input.GetKeyDown(KeyCode.M)) {
            addItem(new Item(0, 5, true, false));
            addItem(new Item(1, 2, true, true));
            addItem(new Item(2, 2, true, true));
        }
        if (Input.GetKeyDown(KeyCode.N)) {
            string str = "";
            for (int i = 0; i < inventory.Count; i++) {
                if (i == inventory.Count-1) {
                    str += inventory[i].itemType;
                }
                else {
                    str += inventory[i].itemType + ", ";
                }
            }
            Debug.Log(str);
        }
    }

    // add an item into the inventory \\
    public void addItem(Item item) {
        if (!item.stackable) {
            inventory.Add(item);
        }
        else if (!checkInventory(item)) {
            inventory.Add(item);
        }
        else {
            inventory[findItem(item)].quantity += item.quantity;
        }
    }

    // checks if an item is in the inventory \\
    private bool checkInventory(Item item) {
        // check each item in inventory
        for (int i = 0; i < inventory.Count; i++) {
            if (inventory[i].itemType == item.itemType) {
                return true;
            }
        }
        return false;
    }

    // finds an item in the inventory and returns its index \\
    private int findItem(Item item) {
        for (int i = 0; i < inventory.Count; i++) {
            if (inventory[i].itemType == item.itemType) {
                return i;
            }
        }
        return -1;
    }

    ///    Methods for saving and loading player inventory    \\\
    public void saveInventory() {
        Debug.Log("Saving Player Inventory");
        SaveSystem.SavePlayerInventory(this);
    }
    public void loadInventory() {
        Debug.Log("Loading Player Inventory");
        InventoryData data = SaveSystem.LoadPlayerInventory();

        for (int i = 0; i < data.playerInventory.Count; i++) {
            inventory.Add(data.playerInventory[i]);
        }
    }
}
