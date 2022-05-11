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
    public GameObject player;

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
            consoleLog.GetComponent<TextMeshProUGUI>().text += "\n-----------------------------------------------------------\n'switchscene' - switches the scene (must input scene name).\n'healparty' - heals all party members.\n'endcombat' - ends the current combat.\n'help' - shows this menu.\n'goto' - moves the player character to a location (e.g temple, well)\ncommands are not case sensitive.\n-----------------------------------------------------------";
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
        // Teleports the player to important locations
        else if (consoleText == "GOTO TEMPLE") {
            player.transform.position = new Vector2(-18, -57);
            consoleLog.GetComponent<TextMeshProUGUI>().text += "\nmoved player to temple";
        }
        else if (consoleText == "GOTO WELL") {
            player.transform.position = new Vector2((float)-19.3, (float)-99.2);
            consoleLog.GetComponent<TextMeshProUGUI>().text += "\nmoved player to well";
        }
        // Ends combat
        else if (consoleText == "ENDCOMBAT" && SceneManager.GetActiveScene().name == "CombatScene") {
            SceneLoader.changeScene = true;
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