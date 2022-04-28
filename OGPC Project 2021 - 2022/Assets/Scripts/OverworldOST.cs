using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OverworldMusicDetection : MonoBehaviour
{
    private CombatManager cM;
    private PlayerProgress PP;
    //Music detection variables
    private bool playingMusic = false;
    // Audio source
    public AudioSource AS;
    //Audio Clips
    public AudioClip FO;
    public AudioClip DO;
    public AudioClip SO;
    public AudioClip PO;
    public AudioClip Play;



    // Start is called before the first frame update

    void Start()
    {
        cM = GameObject.Find("Combat Manager").GetComponent<CombatManager>();
        PP = GameObject.Find("Party Manager").GetComponent<PlayerProgress>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!playingMusic){
            if (PP.playerBiome == PlayerProgress.Biome.desert){
                Play = DO;
            }
            else if (PP.playerBiome == PlayerProgress.Biome.swamp){
                Play = SO;

            }
            else if (PP.playerBiome == PlayerProgress.Biome.forest){
                Play = FO;
            }

            playingMusic = true;
            AS.clip = Play;
            AS.Play(0);

        }
        
        
    }
}
