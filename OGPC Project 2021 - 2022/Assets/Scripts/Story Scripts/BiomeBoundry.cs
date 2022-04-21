using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeBoundry : MonoBehaviour
{
    private PlayerProgress pP;

    // whether the biome is the desert, swamp or forest
    public bool isDesert = false;
    public bool isSwamp = false;
    public bool isForest = false;

    // Start is called before the first frame update
    void Start()
    {
        pP = GameObject.Find("Party and Player Manager").GetComponent<PlayerProgress>();
    }

    // when the player is enters the boundry, update the player biome to match
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            if (isDesert) {
                pP.playerBiome = PlayerProgress.Biome.desert;
            }
            else if (isSwamp) {
                pP.playerBiome = PlayerProgress.Biome.swamp;
            }
            else if (isForest) {
                pP.playerBiome = PlayerProgress.Biome.forest;
            }
        }
    }
}
