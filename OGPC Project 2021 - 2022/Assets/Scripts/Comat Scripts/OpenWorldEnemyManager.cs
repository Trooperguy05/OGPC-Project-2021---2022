using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWorldEnemyManager : MonoBehaviour
{   
    // list of all open world enemies in the scene \\
    public GameObject[] enemies;

    void Start() {
        loadActiveEnemies();
    }

    public void saveActiveEnemies() {
        Debug.Log("Saving Active Enemies");
        SaveSystem.SaveActiveEnemies(this);
    }

    public void loadActiveEnemies() {
        Debug.Log("Loading Active Enemies");
        OverworldEnemyData data = SaveSystem.LoadActiveEnemies();

        for (int i = 0; i < data.overworldEnemyActive.Length; i++) {
            enemies[i].SetActive(data.overworldEnemyActive[i]);
        }
    }
}
