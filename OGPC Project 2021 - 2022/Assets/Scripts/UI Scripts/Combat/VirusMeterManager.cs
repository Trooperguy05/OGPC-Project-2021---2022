using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirusMeterManager : MonoBehaviour
{
    // PC Gameobjects \\
    public GameObject raza;
    public GameObject dorne;
    public GameObject smithson;
    public GameObject zor;
    // Healthbars for the PC's \\
    [Header("Player Healthbars")]
    public GameObject razaVirusBar;
    public GameObject dorneVirusBar;
    public GameObject smithsonVirusBar;
    public GameObject zorVirusBar;
    public Slider razaSlider;
    public Slider dorneSlider;
    public Slider smithsonSlider;
    public Slider zorSlider;

    // access to other scripts
    private PartyStats pS;

    // Start is called before the first frame update
    void Start()
    {
        // cache
        pS = GameObject.Find("Party Manager").GetComponent<PartyStats>();

        // set the position of the virus bars
        razaVirusBar.GetComponent<RectTransform>().position = new Vector2(raza.transform.position.x-1.5f, raza.transform.position.y);
        dorneVirusBar.GetComponent<RectTransform>().position = new Vector2(dorne.transform.position.x-1.5f, dorne.transform.position.y);
        smithsonVirusBar.GetComponent<RectTransform>().position = new Vector2(smithson.transform.position.x-1.5f, smithson.transform.position.y-0.25f);
        zorVirusBar.GetComponent<RectTransform>().position = new Vector2(zor.transform.position.x-1.5f, zor.transform.position.y); 

        // get the virus bar sliders
        razaSlider = razaVirusBar.GetComponent<Slider>();
        dorneSlider = dorneVirusBar.GetComponent<Slider>();
        smithsonSlider = smithsonVirusBar.GetComponent<Slider>();
        zorSlider = zorVirusBar.GetComponent<Slider>();

        // update the sliders to match whatever save of the vMeter the partystats has \\
        razaSlider.value = pS.char1VMeter;
        dorneSlider.value = pS.char2VMeter;
        smithsonSlider.value = pS.char3VMeter;
        zorSlider.value = pS.char4VMeter;
    }

    // method that updates the virus meter based on the value given \\
    // negative amt --> subtract from meter
    // positive amt --> add to meter
    public IEnumerator updateMeter(int amt, Slider slider, string name) {
        // add to the "invisible meter"
        if (name == "raza") {
            pS.char1VMeter += amt;
            if (pS.char1VMeter > 100) {
                pS.char1VMeter = 100;
            }
            else if (pS.char1VMeter < 0) {
                pS.char1VMeter = 0;
            }
        }
        if (name == "dorne") {
            pS.char2VMeter += amt;
            if (pS.char2VMeter > 100) {
                pS.char2VMeter = 100;
            }
            else if (pS.char2VMeter < 0) {
                pS.char2VMeter = 0;
            }
        }
        if (name == "smithson") {
            pS.char3VMeter += amt;
            if (pS.char3VMeter > 100) {
                pS.char3VMeter = 100;
            }
            else if (pS.char3VMeter < 0) {
                pS.char3VMeter = 0;
            }
        }
        if (name == "zor") {
            pS.char4VMeter += amt;
            if (pS.char4VMeter > 100) {
                pS.char4VMeter = 100;
            }
            else if (pS.char4VMeter < 0) {
                pS.char4VMeter = 0;
            }
        }
        // add to the visual meter
        if (slider.value != slider.maxValue || amt < 0) {
            for (int i = 0; i < Mathf.Abs(amt); i++) {
                if (amt < 0) {
                    slider.value--;
                }
                else if (amt > 0) {
                    slider.value++;
                }
                yield return new WaitForSeconds(0.01f);
            }      
        }
    }
}
