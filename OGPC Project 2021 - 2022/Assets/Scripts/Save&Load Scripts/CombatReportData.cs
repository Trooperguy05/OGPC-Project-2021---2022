using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombatReportData
{
    public bool wonLastCombat;

    public CombatReportData(CombatReport cR) {
        wonLastCombat = cR.wonLastCombat;
    }
}
