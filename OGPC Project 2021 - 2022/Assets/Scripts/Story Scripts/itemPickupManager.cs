using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPickupManager : MonoBehaviour
{
    // all pickup items
    public GameObject[] items;

    // load the item data
    void Start()
    {
        loadItems();
    }

    // save and load item data \\
    public void saveItems() {
        Debug.Log("Saving pickup items");
        SaveSystem.saveItemPickups(this);
    }
    public void loadItems() {
        ItemData data = SaveSystem.loadItemPickups();
        if (data != null) {
            for (int i = 0; i < data.items.Length; i++) {
                items[i].SetActive(data.items[i]);
            }
        }
    }
}
