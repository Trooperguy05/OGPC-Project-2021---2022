using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAudio : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource AS;
    public AudioClip regularMenu;
    public AudioClip kazooMenu;
    // Start is called before the first frame update
    void Start()
    {
        // play funny easter egg or regular music
        float percentChance = Random.Range(0,1001);
        if (percentChance == 1) {
            AS.PlayOneShot(kazooMenu, 1);
        }
        else {
            AS.PlayOneShot(regularMenu, 1);
        }
        
    }

}
