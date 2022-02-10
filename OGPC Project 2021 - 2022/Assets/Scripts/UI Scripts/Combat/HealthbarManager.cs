using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarManager : MonoBehaviour
{
    // Healthbars for the PC's \\
    [Header("Player Healthbars")]
    public GameObject razaHealthbar;
    public GameObject dorneHealthbar;
    public GameObject smithsonHealtbar;
    public GameObject zorHealthbar;
    private Slider razaSlider;
    private Slider dorneSlider;
    private Slider smithsonSlider;
    private Slider zorSlider;
    // Healthbars for the enemies \\
    [Header("Enemy Healthbars")]
    public GameObject enemy1Healthbar;
    public GameObject enemy2Healthbar;
    public GameObject enemy3Healthbar;
    public GameObject enemy4Healthbar;
    [Header("Enemy Healthbar Sliders")]
    public Slider enemy1Slider;
    public Slider enemy2Slider;
    public Slider enemy3Slider;
    public Slider enemy4Slider;

    // access to other scripts
    private CombatManager cM;

    // on start-up things \\
    void Start() {
        // cache access to other scripts \\
        cM = GameObject.Find("Combat Manager").GetComponent<CombatManager>();

        // set the initial positions of the healthbars \\
        // PC healthbars
        razaHealthbar.GetComponent<RectTransform>().position = new Vector2(-6, 4);
        dorneHealthbar.GetComponent<RectTransform>().position = new Vector2(-6, 2.6f);
        smithsonHealtbar.GetComponent<RectTransform>().position = new Vector2(-6, 1.2f);
        zorHealthbar.GetComponent<RectTransform>().position = new Vector2(-6, -0.2f);
        // enemy healthbars

        // cache the healthbar sliders \\
        // PC sliders
        razaSlider = razaHealthbar.GetComponent<Slider>();
        dorneSlider = dorneHealthbar.GetComponent<Slider>();
        smithsonSlider = smithsonHealtbar.GetComponent<Slider>();
        zorSlider = zorHealthbar.GetComponent<Slider>();
    }

    // method that shows damage to healthbar \\
    public IEnumerator dealDamage(Slider slider, int amt, float wait) {
        for (int i = 0; i < amt; i++) {
            slider.value -= 1;
            yield return new WaitForSeconds(wait);
        }
    }

    // method that shows healing to healthbar \\
    public IEnumerator giveHeal(Slider slider, int amt, float wait) {
        for (int i = 0; i < amt; i++) {
            slider.value += 1;
            yield return new WaitForSeconds(wait);
        }
    }
}
