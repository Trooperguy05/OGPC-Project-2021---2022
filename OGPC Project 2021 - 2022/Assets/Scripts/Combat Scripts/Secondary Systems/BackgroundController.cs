using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    // the different combat backgrounds
    public Sprite desertBackground;
    public Sprite swampBackground;
    public Sprite forestBackground;

    // if the script has updated the background or not
    public bool updatedBackground;

    // the player progress script
    private PlayerProgress pP;

    // caching the player progress
    void Start() {
        pP = GameObject.Find("Party Manager").GetComponent<PlayerProgress>();
    }

    // Update is called once per frame
    void Update()
    {
        // update the background
        if (!updatedBackground) {
            if (pP.playerBiome == PlayerProgress.Biome.desert) {
                GetComponent<Image>().sprite = desertBackground;
            }
            else if (pP.playerBiome == PlayerProgress.Biome.swamp) {
                GetComponent<Image>().sprite = swampBackground;
            }
            else if (pP.playerBiome == PlayerProgress.Biome.forest) {
                GetComponent<Image>().sprite = forestBackground;
            }
            updatedBackground = true;
        }
    }
}
