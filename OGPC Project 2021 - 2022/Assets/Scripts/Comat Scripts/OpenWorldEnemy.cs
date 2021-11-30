using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenWorldEnemy : MonoBehaviour
{
    // when an enemy interacts with the player
    // initiate combat with the player
    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("Entering Combat");
        FindObjectOfType<PartyStats>().SaveData(); // save party stats
        FindObjectOfType<PlayerProgress>().savePlayerData(); // save player progress
        FindObjectOfType<OpenWorldEnemyManager>().saveActiveEnemies(); // save active enemies
        SceneManager.LoadScene(1); // load the combat scene
    }
}
