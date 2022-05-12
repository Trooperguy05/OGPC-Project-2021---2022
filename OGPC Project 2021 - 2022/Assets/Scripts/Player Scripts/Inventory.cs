using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    // inventory list \\
    public List<Item> inventory = new List<Item>();

    // cheat menu manager \\
    private ConsoleAndAchievementsController cC;
    // Party Stats
    private PartyStats pS;

    // inventory menu \\
    public GameObject invMenu;
    public GameObject scrollableArea;
    public static bool invMenuOpen = false;
    // item prefabs
    public GameObject consumableItem;
    public GameObject nonconsumableItem;
    public GameObject[] prefabs;
    private Button[] buttons;

    // on start-up things \\
    void Start() {
        // get the console controller \\
        cC = GameObject.Find("Console Manager").GetComponent<ConsoleAndAchievementsController>();
        pS = GameObject.Find("Party and Player Manager").GetComponent<PartyStats>();

        // load the inventory on start up \\
        loadInventory();

        // load the inventory in the menu \\
        updateInventoryMenu();
    }

    // allow player to access the inventory menu \\
    void Update() {
        // if the player presses 'i', show the inventory menu
        if (Input.GetKeyDown(KeyCode.I) && !cC.consoleIsActive && loadingScreenManager.loadingDone) {
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
        updateInventoryMenu();
    }

    // checks if an item is in the inventory \\
    public bool checkInventory(Item item) {
        // check each item in inventory
        for (int i = 0; i < inventory.Count; i++) {
            if (inventory[i].itemType == item.itemType) {
                return true;
            }
        }
        return false;
    }

    // finds an item in the inventory and returns its index \\
    public int findItem(Item item) {
        for (int i = 0; i < inventory.Count; i++) {
            if (inventory[i].itemType == item.itemType) {
                return i;
            }
        }
        return -1;
    }

    // method that allows consumable items to be used and then
    // the inventory is updated
    private void useItem(int index) {
        inventory[index].quantity--;
        // if item is a health potion
        if (inventory[index].itemType == Item.ItemType.HealthPotion) {
            pS.char1HP += 15;
            pS.char2HP += 15;
            pS.char3HP += 15;
            pS.char4HP += 15;
            if (pS.char1HP > pS.char1HPMax) { pS.char1HP = pS.char1HPMax; }
            if (pS.char2HP > pS.char2HPMax) { pS.char2HP = pS.char2HPMax; }
            if (pS.char3HP > pS.char3HPMax) { pS.char3HP = pS.char3HPMax; }
            if (pS.char4HP > pS.char4HPMax) { pS.char4HP = pS.char4HPMax; }
        }
        // if the quantity is 0, remove it from the inventory
        if (inventory[index].quantity <= 0) {
            inventory.RemoveAt(index);
        }
        updateInventoryMenu();
    }

    // method that removes a large quantity of a stackable item from the inventory
    public void removeItem(int index, int amt) {
        inventory[index].quantity -= amt;
        if (inventory[index].quantity <= 0) {
            inventory.RemoveAt(index);
        }
        updateInventoryMenu();
    }

    // method for the "Use Button" to allow for the use of the
    // useItem method from UI
    public void btnUseItem() {
        for (int i = 0; i < inventory.Count; i++) {
            if (EventSystem.current.currentSelectedGameObject.transform.parent.name == inventory[i].getName()) {
                useItem(i);
                return;
            }
        }
    }

    // updates the inventory menu \\
    private void updateInventoryMenu() {
        // reset by deleting the previous prefabs
        for (int i = 0; i < prefabs.Length; i++) {
            Destroy(prefabs[i]);
        }
        // instantiate new prefabs
        prefabs = new GameObject[inventory.Count];
        buttons = new Button[inventory.Count];
        for (int i = 0; i < inventory.Count; i++) {
            if (inventory[i].isConsumable()) {
                // create the prefab
                GameObject temp = Instantiate(consumableItem);
                RectTransform rt = temp.GetComponent<RectTransform>();
                Button btn = temp.transform.Find("Use Button").GetComponent<Button>();
                temp.transform.SetParent(scrollableArea.transform, false);
                rt.transform.localPosition = new Vector3(rt.transform.localPosition.x, 131 - (i * 134), 0);
                prefabs[i] = temp;
                buttons[i] = btn;
                // update the prefab to the item's information
                temp.name = inventory[i].getName();
                TextMeshProUGUI itemName = temp.transform.Find("Item Name").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI itemQuantity = temp.transform.Find("Quantity").GetComponent<TextMeshProUGUI>();
                itemName.text = inventory[i].getName();
                itemQuantity.text = "x" + inventory[i].quantity;
                btn.onClick.AddListener(btnUseItem);
            }
            else {
                Debug.Log("h");
                // create the prefab
                GameObject temp = Instantiate(nonconsumableItem);
                RectTransform rt = temp.GetComponent<RectTransform>();
                temp.transform.SetParent(scrollableArea.transform, false);
                rt.transform.localPosition = new Vector3(rt.transform.localPosition.x, 131 - (i * 134), 0);
                prefabs[i] = temp;
                // update the prefab to the item's information
                temp.name = inventory[i].getName();
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

        if (data != null) {
            for (int i = 0; i < data.playerInventory.Count; i++) {
                inventory.Add(data.playerInventory[i]);
            }  
        }
    }
}
