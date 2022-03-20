using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{

    public Rigidbody2D name1;
    public Rigidbody2D name2;
    public Rigidbody2D name3;
    public Rigidbody2D name4;
    public Rigidbody2D croc;
    public GameObject cam1;
    public GameObject crocObject;

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(gravityON());
        
    }

    public IEnumerator gravityON() {
        while (name1.position.y <= 2) {
            name1.MovePosition(name1.position + new Vector2(0f, 0.05f));
            name2.MovePosition(name2.position + new Vector2(0f, 0.05f));
            name3.MovePosition(name3.position + new Vector2(0f, 0.05f));
            name4.MovePosition(name4.position + new Vector2(0f, 0.05f));
            yield return null;
        }
        yield return new WaitForSeconds((float)9.2);
        name1.isKinematic = false;
        name2.isKinematic = false;
        name3.isKinematic = false;
        name4.isKinematic = false;
        name2.AddForce(new Vector2(-1000f, 0f));
        name3.AddForce(new Vector2(1000f, 0f));
        name4.AddForce(new Vector2(0f, 1000f));

        yield return new WaitForSeconds(5);
        croc.isKinematic = false;
        croc.AddForce(new Vector2(0f, 350f));
    }
}
