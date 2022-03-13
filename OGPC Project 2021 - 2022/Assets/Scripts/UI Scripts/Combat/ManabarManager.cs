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

    // set-up \\
    void Start() {
        // get the sliders
        smithsonManabarSlider = smithsonManabar.GetComponent<Slider>();
        // move the manabars
        smithsonManabar.GetComponent<RectTransform>().position = new Vector2(smithson.transform.position.x-1.5f, smithson.transform.position.y);
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
