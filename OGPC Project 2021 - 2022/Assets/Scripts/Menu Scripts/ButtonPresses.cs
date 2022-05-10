using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPresses : MonoBehaviour
{
    public AudioClip buttonPressed;
    public AudioSource AS;
    public void buttonPress() {
        AS.PlayOneShot(buttonPressed);
    }
}
