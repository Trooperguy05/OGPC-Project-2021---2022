using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAudio : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource AS;
    public AudioClip regularMenu;
    public AudioClip kazooMenu;
    public AudioClip play;
    // Start is called before the first frame update
    void Start()
    {
        // play funny easter egg or regular music
        float percentChance = Random.Range(0,1001);
        if (percentChance == 1) {
            play = kazooMenu;
            //AS.PlayOneShot(kazooMenu, 1);
        }
        else {
            play = regularMenu;
            //AS.PlayOneShot(regularMenu, 1);
        }

        AS.clip = play;
        AS.Play(0);
        
    }

}
