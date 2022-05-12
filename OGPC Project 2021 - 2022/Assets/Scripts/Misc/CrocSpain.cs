using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocSpain : MonoBehaviour
{
    public GameObject croc;
    public double crocSpin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        croc.transform.Rotate(0f, 0f, (float) (crocSpin), Space.Self);
        if (Input.GetMouseButton(0)) {
            crocSpin += 0.1;
        }
    }
}
