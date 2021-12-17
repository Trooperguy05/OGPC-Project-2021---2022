using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // main inventory list
    public List<Item> inventory = new List<Item>();

    void Update() {
        if (Input.GetKeyDown(KeyCode.K)) {
            addItem(new Item("Potion", true, 1));
            
            string str =  "";
            for (int i = 0; i < inventory.Count; i++) {
                if (i != inventory.Count - 1) {
                    str += inventory[i].name + ", ";
                }
                else {
                    str += inventory[i].name;
                }
            }
            Debug.Log(str);
        }
    }

    // method that adds an item to inventory \\
    public void addItem(Item item) {
        // if the item already exists add to the quantity of the item in the inventory
        if (inventory.Contains(item)) {
            inventory[inventory.IndexOf(item)].quantity += item.quantity;
        }
        else {
            inventory.Add(item);
        }
    }
}
