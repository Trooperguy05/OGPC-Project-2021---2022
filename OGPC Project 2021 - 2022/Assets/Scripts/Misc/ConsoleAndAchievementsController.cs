using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ConsoleAndAchievementsController : MonoBehaviour
{
    // Declaring Varibles \\
    public GameObject console;
    public bool consoleIsActive;
    public string consoleText;
    public GameObject inputField;
    public GameObject consoleLog;
    public GameObject achievements;
    public static bool achievementsIsActive;

    void Update() {
        // Opens and closes the console \\
        if (Input.GetKeyDown(KeyCode.BackQuote)) {
            consoleIsActive = !consoleIsActive;
            console.SetActive(consoleIsActive); 
            // player cannot move while console menu is open
            PlayerMovement.playerAbleMove = !consoleIsActive;
        }
    }

    // Stores the input of the text box \\
    public void storeInput() {
        consoleText = inputField.GetComponent<TMP_InputField>().text;
        checkCommand();
    }

    // Checks if a valid command has been entered \\
    private void checkCommand() {
        // Forces the stored input into full uppercase
        consoleText = consoleText.ToUpper();
        // Displays the help menu for the console in the console log
        if (consoleText.Equals("HELP")) {
            consoleLog.GetComponent<TextMeshProUGUI>().text += "\n*\nswitchscene - must add scene name.\nskipturn - skips an enemies turn during combat.\nhealparty - heals all party members.\nendcombat - ends the current combat.\nhelp - shows this menu.\ncommands are not case sensitive.\n*";
        }
        // Switchs the scene based on what scene name is entered with the commands
        else if (consoleText == "SWITCHSCENE MAINMENU") {
            SceneManager.LoadScene(0);
        }
        else if (consoleText == "SWITCHSCENE OVERWORLD") {
            SceneManager.LoadScene(1);
        }
        else if (consoleText == "SWITCHSCENE COMBAT") {
            SceneManager.LoadScene(2);
        }
        // Displays that an invalid command is entered in the console
        else {
            consoleLog.GetComponent<TextMeshProUGUI>().text += "\ninvalid command issued to the console";
        }
    }

    //Opens and closes the achievements menu \\
    public void toggleAchievementsMenu() {
        achievementsIsActive = !achievementsIsActive;
        achievements.SetActive(achievementsIsActive);
    }
}