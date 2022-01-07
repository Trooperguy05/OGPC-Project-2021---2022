using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScroller : MonoBehaviour
{
    public static Scrollbar scrollbar;
    public Inventory inv;
    private float lastValue = 0f;

    void Start() {
        scrollbar = GetComponent<Scrollbar>();
        inv = GameObject.Find("OverworldPlayerCharacter").GetComponent<Inventory>();
        scrollbar.onValueChanged.AddListener(moveMenuElements);
    }

    public void moveMenuElements(float value) {
        for (int i = 0; i < inv.prefabs.Length; i++) {
            RectTransform rt = inv.prefabs[i].GetComponent<RectTransform>();
            if (lastValue > value) {
                Debug.Log("Scrolling UP");
                rt.position = new Vector3(rt.position.x, rt.position.y - (inv.prefabs.Length), 0);
            }   
            else if (lastValue < value) {
                Debug.Log("Scrolling DOWN");
                rt.position = new Vector3(rt.position.x, rt.position.y + (inv.prefabs.Length), 0);
            }
        }
        lastValue = value;
    }
}
