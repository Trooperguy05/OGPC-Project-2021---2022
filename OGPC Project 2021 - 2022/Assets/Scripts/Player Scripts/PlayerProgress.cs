using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerProgress : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;

    // details what biome the player is in
    public enum Biome {
        desert,
        swamp,
        forest
    }
    public Biome playerBiome = Biome.desert;

    [Header("Story Progress Variables")]
    public bool passedTutorial = false;
    public bool destroyedDesertPhylactery = false;
    public bool destroyedSwampPhylactery = false;

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
            if (player != null) {
                player.transform.position = new Vector3(data.playerPosition[0], data.playerPosition[1], data.playerPosition[2]);
            }
            
            // load the player biome
            playerBiome = data.playerBiome;

            // load the story progress
            passedTutorial = data.passedTutorial;
            destroyedDesertPhylactery = data.destroyedDesertPhylactery;
            destroyedSwampPhylactery = data.destroyedSwampPhylactery;
        }
    }
}
