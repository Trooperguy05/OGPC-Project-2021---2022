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
    public Sprite forestBackground_noRiver;

    // if the script has updated the background or not
    public bool updatedBackground;

    // the player progress script
    private PlayerProgress pP;
    private CombatManager cM;

    // caching the player progress
    void Start() {
        pP = GameObject.Find("Party Manager").GetComponent<PlayerProgress>();
        cM = GameObject.Find("Combat Manager").GetComponent<CombatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // update the background
        if (!updatedBackground) {
            if (cM.specifiedEnemy == 10) {
                GetComponent<Image>().sprite = forestBackground_noRiver;
            }
            else if (pP.playerBiome == PlayerProgress.Biome.desert) {
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
