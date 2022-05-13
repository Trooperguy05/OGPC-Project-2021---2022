using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPresses : MonoBehaviour
{
    public AudioClip buttonPressed;
    public AudioSource AS;

    // method that will play the button sfx when pressed
    public void buttonPress() {
        AS.PlayOneShot(buttonPressed);
    }
}
