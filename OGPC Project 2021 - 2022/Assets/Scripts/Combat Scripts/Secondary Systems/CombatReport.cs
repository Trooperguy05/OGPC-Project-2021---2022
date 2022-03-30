using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatReport : MonoBehaviour
{
    public bool wonLastCombat = false;

    // method to save data
    public void saveData() {
        SaveSystem.saveCombatReport(this);
    }
    
    // method to load the save datas
    public void loadData() {
        CombatReportData cRD = SaveSystem.loadCombatReport();

        if (cRD != null) {
            wonLastCombat = cRD.wonLastCombat;
        }
    }
}
