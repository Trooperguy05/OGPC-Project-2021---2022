using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManabarManager : MonoBehaviour
{
    [Header("PC Objects")]
    public GameObject smithson;
    [Header("Manabars")]
    public GameObject smithsonManabar;
    public Slider smithsonManabarSlider;

    // access to other script
    private PartyStats pS;

    // set-up \\
    void Start() {
        // party stats
        pS = GameObject.Find("Party Manager").GetComponent<PartyStats>();
        // get the sliders
        smithsonManabarSlider = smithsonManabar.GetComponent<Slider>();
        // move the manabars
        smithsonManabar.GetComponent<RectTransform>().position = new Vector2(smithson.transform.position.x-1.5f, smithson.transform.position.y);
    }

    // method that updates the manabar to match the mana in party stats \\
    public void updateManabar() {
        smithsonManabarSlider.value = pS.char3Mana;
    }

    // method that shows mana depletion \\
    public IEnumerator depleteMana(Slider slider, int amt, float wait) {
        for (int i = 0; i < amt; i++) {
            slider.value -= 1;
            yield return new WaitForSeconds(wait);
        }
    }

    // method taht shows mana regeneration \\
    public IEnumerator regenMana(Slider slider, int amt, float wait) {
        for (int i = 0; i < amt; i++) {
            slider.value += 1;
            yield return new WaitForSeconds(wait);
        }
    }
}
