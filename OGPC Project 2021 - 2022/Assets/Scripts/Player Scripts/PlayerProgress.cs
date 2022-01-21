using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgress : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;

    [Header("Story Progress Variables")]
    public bool passedTutorial = false;

    // save the player progress \\
    public void savePlayerData() {
        Debug.Log("Saving Player Data");
        SaveSystem.SavePlayerProgress(this);
    }

    // load the player progress \\
    public void loadPlayerData() {
        Debug.Log("Loading Player Data");
        PlayerData data = SaveSystem.LoadPlayerProgress();
        if (data != null) {
            // load the player position
            player.transform.position = new Vector3(data.playerPosition[0], data.playerPosition[1], data.playerPosition[2]);

            // load the story progress
            passedTutorial = data.passedTutorial;
        }
    }
}
