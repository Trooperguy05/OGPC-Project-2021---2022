using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpecifiedEnemyData
{
    public int specifiedEnemy;

    public SpecifiedEnemyData(OpenWorldEnemy oE) {
        specifiedEnemy = oE.specifiedEnemy;
    }
}
