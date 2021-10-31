using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    // player gameobject
    public float[] playerPosition = new float[3];

    // player progress variables
    public bool passedTutorial;

    public PlayerData(PlayerProgress playerProgress) {
        // player positional data
        playerPosition[0] = playerProgress.player.transform.position.x;
        playerPosition[1] = playerProgress.player.transform.position.y;
        playerPosition[2] = playerProgress.player.transform.position.z;

        // player progress data
        passedTutorial = playerProgress.passedTutorial;
    }
}
