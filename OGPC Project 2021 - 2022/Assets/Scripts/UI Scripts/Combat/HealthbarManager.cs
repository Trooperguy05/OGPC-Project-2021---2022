using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarManager : MonoBehaviour
{
    // PC healthbars and sliders \\
    [Header("Player Healthbars")]
    public GameObject razaHealthbar;
    public GameObject dorneHealthbar;
    public GameObject smithsonHealthbar;
    public GameObject zorHealthbar;
    private Slider razaSlider;
    private Slider dorneSlider;
    private Slider smithsonSlider;
    private Slider zorSlider;

    // Start is called before the first frame update
    void Start()
    {
        razaSlider = razaHealthbar.GetComponent<Slider>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Y)) {
            StartCoroutine(damageHealthbar(razaSlider, 25, 0.1f));
        }
        else if (Input.GetKeyDown(KeyCode.T)) {
            StartCoroutine(healHealthbar(razaSlider, 15, 0.05f));
        }
    }

    // a method that will, over time, show the damage the person has taken \\
    public IEnumerator damageHealthbar(Slider slider, int amt, float speed) {
        for (int i = 0; i < amt; i++) {
            slider.value -= 1;
            if (slider.value <= slider.minValue) {
                break;
            }
            yield return new WaitForSeconds(speed);
        }
    }

    // a method that will, over time, show the amount of healing a person has gained \\
    public IEnumerator healHealthbar(Slider slider, int amt, float speed) {
        for (int i = 0; i < amt; i++) {
            slider.value += 1;
            if (slider.value >= slider.maxValue) {
                break;
            }
            yield return new WaitForSeconds(speed);
        }
    }
}
