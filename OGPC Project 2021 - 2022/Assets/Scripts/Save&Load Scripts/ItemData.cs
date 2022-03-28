using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public bool[] items;

    public ItemData(itemPickupManager iPM) {
        items = new bool[iPM.items.Length];
        for (int i = 0; i < iPM.items.Length; i++) {
            items[i] = iPM.items[i].activeSelf;
        }
    }
}
