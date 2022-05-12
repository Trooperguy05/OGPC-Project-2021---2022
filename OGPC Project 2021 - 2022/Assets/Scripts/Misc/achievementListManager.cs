using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class achievementListManager : MonoBehaviour
{
    public GameObject achievementList;
    private achievements achs;
    private ConsoleAndAchievementsController cC;

    [Header("Achievement Images")]
    public GameObject pausePwner_image;
    public GameObject crocSpin_image;
    public GameObject fileRemover_image;

    public static bool listOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        achs = GetComponent<achievements>();
        cC = GameObject.Find("Console Manager").GetComponent<ConsoleAndAchievementsController>();
    }

    // Update is called once per frame
    void Update()
    {  
        // open the achievment list
        if (!DialogueManager.InDialogue && loadingScreenManager.loadingDone && !cC.consoleIsActive && !Inventory.invMenuOpen) {
            if (Input.GetKeyDown(KeyCode.Backspace)) {
                listOpen = !listOpen;
                achievementList.SetActive(listOpen);

                // disable achievements if the player doesn't have them
                if (!achs.pausePwner) {
                    pausePwner_image.SetActive(false);
                }
                else {
                    pausePwner_image.SetActive(true);
                }
                if (!achs.crocSpin) {
                    crocSpin_image.SetActive(false);
                }
                else {
                    crocSpin_image.SetActive(true);
                }
                if (!achs.fileRemover) {
                    fileRemover_image.SetActive(false);
                }
                else {
                    fileRemover_image.SetActive(true);
                }
            }
        }
    }
}
