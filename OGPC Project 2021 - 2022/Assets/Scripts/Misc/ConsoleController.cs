using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConsoleController : MonoBehaviour
{
    // Declaring Varibles \\
    public GameObject console;
    public bool consoleIsActive;
    public string consoleText;
    public GameObject inputField;
    public GameObject consoleLog;

    void Update() {
        // Opens and closes the console \\
        if (Input.GetKeyDown(KeyCode.BackQuote)) {
            consoleIsActive = !consoleIsActive;
            console.SetActive(consoleIsActive); 
            // player cannot move while console menu is open
            PlayerMovement.playerAbleMove = !consoleIsActive;
        };
        if (Input.GetKeyDown(KeyCode.Return)) {
            storeInput();
        }
    }

    // Stores the input of the text box \\
    public void storeInput() {
        consoleText = inputField.GetComponent<Text>().text;
        checkCommand();
        inputField.GetComponent<Text>().text = null;
    }

    // Checks if a valid command has been entered \\
    private void checkCommand() {
        // Forces the stored input into full uppercase
        consoleText = consoleText.ToUpper();
        // Displays the help menu for the console in the console log
        if (consoleText == "HELP") {
            consoleLog.GetComponent<Text>().text += "\n*\nSwitchScene - Must add scene name.\nSkipTurn - Skips an enemies turn during combat.\nHealParty - Heals all party members.\nEndCombat - Ends the current combat.\nHelp - Shows this menu.\nCommands are not case sensitive.\n*";
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
        else if (consoleText == "SWITCHSCENE CREDITS") {
            SceneManager.LoadScene(4);
        }
        // Displays that an invalid command is entered in the console
        else {
            consoleLog.GetComponent<Text>().text += "\nInvalid command issued to the console";
        }
    }
}
