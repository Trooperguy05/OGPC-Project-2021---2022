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
            if (cM.initiativeNames.Contains("Worm") || cM.initiativeNames.Contains("Mantrap") || cM.initiativeNames.Contains("Giant")){
                AS.PlayOneShot(MC, 1);
            }
            else if (PP.playerBiome == PlayerProgress.Biome.desert){
                AS.PlayOneShot(DC, 1);
            }
            else if (PP.playerBiome == PlayerProgress.Biome.swamp){
                AS.PlayOneShot(SC, 1);
            }
            else if (PP.playerBiome == PlayerProgress.Biome.forest){
                AS.PlayOneShot(FC, 1);
            }

            playingMusic = true;

        }
        
    }
}
