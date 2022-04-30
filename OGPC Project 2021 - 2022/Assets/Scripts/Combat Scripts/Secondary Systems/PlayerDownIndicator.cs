using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDownIndicator : MonoBehaviour
{
    // sprite renderer of pc
    public SpriteRenderer sR;
    private PartyStats pS;

    // Start is called before the first frame update
    void Start()
    {
        sR = GetComponent<SpriteRenderer>();
        pS = GameObject.Find("Party Manager").GetComponent<PartyStats>();
    }

    // change the sprite's color to black if the pc is downed
    void Update() {
        // if the pc is raza
        if (gameObject.name == "Raza") {
            if (pS.char1HP <= 0) {
                sR.color = new Color(0, 0, 0, 1);
            }
            else {
                sR.color = new Color(255, 255, 255, 1);
            }
        }
        // dorne
        else if (gameObject.name == "Dorne") {
            if (pS.char2HP <= 0) {
                sR.color = new Color(0, 0, 0, 1);
            }
            else {
                sR.color = new Color(255, 255, 255, 1);
            }
        }
        // smithson
        else if (gameObject.name == "Smithson") {
            if (pS.char3HP <= 0) {
                sR.color = new Color(0, 0, 0, 1);
            }
            else {
                sR.color = new Color(255, 255, 255, 1);
            }
        }
        // zor
        else if (gameObject.name == "Zor") {
            if (pS.char4HP <= 0) {
                sR.color = new Color(0, 0, 0, 1);
            }
            else {
                sR.color = new Color(255, 255, 255, 1);
            }
        }
    }
}
