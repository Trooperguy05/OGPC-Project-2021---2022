using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWorldEnemyManager : MonoBehaviour
{   
    // list of all open world enemies in the scene \\
    public GameObject[] enemies;

    // at the start of the program, load active enemies
    void Start() {
        loadActiveEnemies();
    }

    // save active enemies
    public void saveActiveEnemies() {
        Debug.Log("Saving Active Enemies");
        SaveSystem.SaveActiveEnemies(this);
    }

    // loads active enemies
    public void loadActiveEnemies() {
        Debug.Log("Loading Active Enemies");
        OverworldEnemyData data = SaveSystem.LoadActiveEnemies();
        if (data != null) {
            for (int i = 0; i < data.overworldEnemyActive.Length; i++) {
                enemies[i].SetActive(data.overworldEnemyActive[i]);
            }  
        }
    }
}
