using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarManager : MonoBehaviour
{
    // PC Gameobjects \\
    public GameObject raza;
    public GameObject dorne;
    public GameObject smithson;
    public GameObject zor;
    // Healthbars for the PC's \\
    [Header("Player Healthbars")]
    public GameObject razaHealthbar;
    public GameObject dorneHealthbar;
    public GameObject smithsonHealtbar;
    public GameObject zorHealthbar;
    public Slider razaSlider;
    public Slider dorneSlider;
    public Slider smithsonSlider;
    public Slider zorSlider;
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
        updatePlayerHealthbars();
        // enemy healthbars
        updateEnemyHealthbars();

        // cache the healthbar sliders \\
        // PC sliders
        razaSlider = razaHealthbar.GetComponent<Slider>();
        dorneSlider = dorneHealthbar.GetComponent<Slider>();
        smithsonSlider = smithsonHealtbar.GetComponent<Slider>();
        zorSlider = zorHealthbar.GetComponent<Slider>();
    }

    // method that updates the position of the enemy healthbars \\
    public void updateEnemyHealthbars() {
        enemy1Healthbar.GetComponent<RectTransform>().position = new Vector2(cM.enemy1.transform.position.x+1, cM.enemy1.transform.position.y+0.25f);
        enemy2Healthbar.GetComponent<RectTransform>().position = new Vector2(cM.enemy2.transform.position.x+1, cM.enemy2.transform.position.y+0.25f);
        enemy3Healthbar.GetComponent<RectTransform>().position = new Vector2(cM.enemy3.transform.position.x+1, cM.enemy3.transform.position.y+0.25f);
        enemy4Healthbar.GetComponent<RectTransform>().position = new Vector2(cM.enemy4.transform.position.x+1, cM.enemy4.transform.position.y+0.25f);
    }
    // method that updates the position of the player healthbars \\
    public void updatePlayerHealthbars() {
        razaHealthbar.GetComponent<RectTransform>().position = new Vector2(raza.transform.position.x-1, raza.transform.position.y+0.25f);
        dorneHealthbar.GetComponent<RectTransform>().position = new Vector2(dorne.transform.position.x-1, dorne.transform.position.y+0.25f);
        smithsonHealtbar.GetComponent<RectTransform>().position = new Vector2(smithson.transform.position.x-1, smithson.transform.position.y+0.25f);
        zorHealthbar.GetComponent<RectTransform>().position = new Vector2(zor.transform.position.x-1, zor.transform.position.y+0.25f); 
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
