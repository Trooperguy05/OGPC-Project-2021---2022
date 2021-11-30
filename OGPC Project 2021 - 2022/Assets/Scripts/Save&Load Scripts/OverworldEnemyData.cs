using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OverworldEnemyData
{
    public bool[] overworldEnemyActive;

    public OverworldEnemyData(OpenWorldEnemyManager eM) {
        overworldEnemyActive = new bool[eM.enemies.Length];
        for (int i = 0; i < eM.enemies.Length; i++) {
            overworldEnemyActive[i] = eM.enemies[i].activeSelf;
        }
    }
}
