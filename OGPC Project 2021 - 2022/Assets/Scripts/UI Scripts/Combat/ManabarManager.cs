using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManabarManager : MonoBehaviour
{
    [Header("Manabars")]
    public GameObject smithsonManabar;
    public GameObject zorManabar;
    public Slider smithsonManabarSlider;
    public Slider zorManabarSlider;

    // set-up \\
    void Start() {
        // get the sliders
        smithsonManabarSlider = smithsonManabar.GetComponent<Slider>();
        zorManabarSlider = zorManabar.GetComponent<Slider>();
        // move the manabars
        smithsonManabar.GetComponent<RectTransform>().position = new Vector2(-6, 0.95f);
        zorManabar.GetComponent<RectTransform>().position = new Vector2(-6, -0.45f);
    }

    // method that shows mana depletion \\
    public IEnumerator depleteMana(Slider slider, int amt, float wait) {
        for (int i = 0; i < amt; i++) {
            slider.value -= 1;
            yield return new WaitForSeconds(wait);
        }
        Debug.Log("j");
    }

    // method taht shows mana regeneration \\
    public IEnumerator regenMana(Slider slider, int amt, float wait) {
        for (int i = 0; i < amt; i++) {
            slider.value += 1;
            yield return new WaitForSeconds(wait);
        }
    }
}
