using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MusicDetection : MonoBehaviour
{
    private CombatManager cM;
    private PlayerProgress PP;
    //Music detection variables
    private bool playingMusic = false;
    // Audio source
    public AudioSource AS;
    //Audio Clips
    public AudioClip FC;
    public AudioClip DC;
    public AudioClip SC;
    public AudioClip MC;
    public AudioClip BC;
    public AudioClip Play;

    // cache
    void Start()
    {
        cM = GameObject.Find("Combat Manager").GetComponent<CombatManager>();
        PP = GameObject.Find("Party Manager").GetComponent<PlayerProgress>();

    }

    // update the music to play
    void Update()
    {
        if (!playingMusic) {
            StartCoroutine(wait());
        }
    }

    // wait 0.01 seconds before updating the music
    IEnumerator wait() {
        yield return new WaitForSeconds(0.01f);

        if (!playingMusic){
            if (cM.initiativeNames.Contains("Dragon")) {
                Play = BC;
            }
            else if (cM.initiativeNames.Contains("Worm") || cM.initiativeNames.Contains("Man Trap") || cM.initiativeNames.Contains("Giant")){
                Play = MC;
            }
            else if (PP.playerBiome == PlayerProgress.Biome.desert){
                Play = DC;
            }
            else if (PP.playerBiome == PlayerProgress.Biome.swamp){
                Play = SC;
            }
            else if (PP.playerBiome == PlayerProgress.Biome.forest){
                Play = FC;
            }

            playingMusic = true;
            AS.clip = Play;
            AS.Play(0);
        }
    }
}
