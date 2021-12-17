using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    // basic item properties
    public string name;
    public bool consumable;
    public int quantity;

    // item constructor
    public Item(string n, bool c, int q) {
        this.name = n;
        this.consumable = c;
        this.quantity = q;
    }

    // to string method
    public string toString() {
        return "Name: " + name + "\n Consumable: " + consumable + "\n Quantity: " + quantity;
    }
}
