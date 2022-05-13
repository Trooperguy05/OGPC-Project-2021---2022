using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    // name of people talking
    public string name;
    public string[] names;

    // what each person is saying
    [TextArea(3, 10)]
    public string[] sentences;
}
