using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    // inventory list \\
    public List<Item> inventory = new List<Item>();

    // inventory menu \\
    public GameObject invMenu;
    private bool invMenuOpen = false;
    // item prefabs
    public GameObject consumableItem;
    public GameObject nonconsumableItem;
    private GameObject[] prefabs;

    // load the inventory on start up \\
    void Start() {
        loadInventory();

        addItem(new Item(Item.ItemType.HealthPotion, 2, true, true));
        addItem(new Item(Item.ItemType.ManaPotion, 2, true, true));

        // load the inventory in the menu \\
        prefabs = new GameObject[inventory.Count];
        for (int i = 0; i < inventory.Count; i++) {
            if (inventory[i].isConsumable()) {
                // create the prefab
                GameObject temp = Instantiate(consumableItem);
                RectTransform rt = temp.GetComponent<RectTransform>();
                temp.transform.SetParent(invMenu.transform, false);
                rt.transform.localPosition = new Vector3(rt.transform.localPosition.x, 131 - (i * 134), 0);
                prefabs[i] = temp;
                // update the prefab to the item's information
                TextMeshProUGUI itemName = temp.transform.Find("Item Name").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI itemQuantity = temp.transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
                itemName.text = inventory[i].getName();
                itemQuantity.text = "x" + inventory[i].quantity;
            }
        }
    }

    // testing
    void Update() {
        // if the player presses 'i', show the inventory menu
        if (Input.GetKeyDown(KeyCode.I)) {
            invMenuOpen = !invMenuOpen;
            invMenu.SetActive(invMenuOpen);
            PlayerMovement.playerAbleMove = !invMenuOpen;
        }

        // prints items in inventory
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

    // updates the inventory menu \\
    private void updateInventoryMenu() {
        // reset by deleting the previous prefabs
        for (int i = 0; i < prefabs.Length; i++) {
            Destroy(prefabs[i]);
        }
        // instantiate new prefabs
        prefabs = new GameObject[inventory.Count];
        for (int i = 0; i < inventory.Count; i++) {
            if (inventory[i].isConsumable()) {
                // create the prefab
                GameObject temp = Instantiate(consumableItem);
                RectTransform rt = temp.GetComponent<RectTransform>();
                temp.transform.SetParent(invMenu.transform, false);
                rt.transform.localPosition = new Vector3(rt.transform.localPosition.x, 131 - (i * 134), 0);
                prefabs[i] = temp;
                // update the prefab to the item's information
                TextMeshProUGUI itemName = temp.transform.Find("Item Name").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI itemQuantity = temp.transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
                itemName.text = inventory[i].getName();
                itemQuantity.text = "x" + inventory[i].quantity;
            }
        }
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
