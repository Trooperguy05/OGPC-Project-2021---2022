using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    // player gameobject
    public float[] playerPosition = new float[3];

    // player biome
    public PlayerProgress.Biome playerBiome;

    // player progress variables
    public bool wonLastCombat;
    public bool passedTutorial;
    public bool destroyedDesertPhylactery;
    public bool destroyedSwampPhylactery;

    public PlayerData(PlayerProgress playerProgress) {
        // player positional data
        playerPosition[0] = playerProgress.player.transform.position.x;
        playerPosition[1] = playerProgress.player.transform.position.y;
        playerPosition[2] = playerProgress.player.transform.position.z;

        // player biome data
        playerBiome = playerProgress.playerBiome;

        // player progress data
        passedTutorial = playerProgress.passedTutorial;
        destroyedDesertPhylactery = playerProgress.destroyedDesertPhylactery;
        destroyedSwampPhylactery = playerProgress.destroyedSwampPhylactery;
    }
}
