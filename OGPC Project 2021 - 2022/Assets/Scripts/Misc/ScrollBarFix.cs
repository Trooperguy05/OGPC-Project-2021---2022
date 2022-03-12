using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBarFix : MonoBehaviour
{
    public GameObject bar;
    public Scrollbar scrollBar;
    // Start is called before the first frame update
    public void setValue()
    {
        scrollBar.value = 1;
    }

}
