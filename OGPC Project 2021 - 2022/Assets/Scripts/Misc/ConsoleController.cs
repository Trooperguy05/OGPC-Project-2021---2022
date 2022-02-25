using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleController : MonoBehaviour
{
    public string consoleText;
    public GameObject inputField;
    public GameObject consoleLog;

    public void storeInput() {
        consoleText = inputField.GetComponent<Text>().text;
        checkCommand();
    }

    private void checkCommand() {
        consoleText = consoleText.ToUpper();
        if (consoleText == "HELP") {
            consoleLog.GetComponent<Text>().text += "*\nSwitchScene - Must add scene name.\nSkipTurn - Skips an enemies turn during combat.\nHealParty - Heals all party members.\nEndCombat - Ends the current combat.\nHelp - Shows this menu.\nCommands are not case sensitive.\n*";
        }
    }
}
