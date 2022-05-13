using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OverworldOST : MonoBehaviour
{
    private PlayerProgress PP;
    //Music detection variables
    public bool playingMusic = false;

    // Audio source
    public AudioSource AS;
    //Audio Clips
    public AudioClip FO;
    public AudioClip DO;
    public AudioClip SO;
    public AudioClip PO;
    public AudioClip Play;
    
    // caching
    void Start()
    {
        PP = GameObject.Find("Party and Player Manager").GetComponent<PlayerProgress>();
    }

    // update the music base on the biome the player is in
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
