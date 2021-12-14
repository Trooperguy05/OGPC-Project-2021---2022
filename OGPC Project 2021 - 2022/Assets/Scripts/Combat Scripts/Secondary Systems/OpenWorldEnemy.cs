using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenWorldEnemy : MonoBehaviour
{
    // making so a specified enemy has to be in the combat
    public int specifiedEnemy = -1; // -1 if none, positive if there is

    // when an enemy interacts with the player
    // initiate combat with the player
    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("Entering Combat");
        FindObjectOfType<PartyStats>().SaveData(); // save party stats
        FindObjectOfType<PlayerProgress>().savePlayerData(); // save player progress
        FindObjectOfType<OpenWorldEnemyManager>().saveActiveEnemies(); // save active enemies
        SaveSystem.SaveSpecifiedEnemy(this); // save the specified enemy
        SceneManager.LoadScene(2); // load the combat scene
    }
}
