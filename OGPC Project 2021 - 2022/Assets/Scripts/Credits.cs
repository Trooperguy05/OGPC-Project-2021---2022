using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{

    public Rigidbody2D name1;
    public Rigidbody2D name2;
    public Rigidbody2D name3;
    public Rigidbody2D name4;

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(gravityON());
        
    }

    public IEnumerator gravityON() {
        while (name1.position.y <= 0.05) {
            name1.MovePosition(name1.position + new Vector2(0f, 0.05f));
            name2.MovePosition(name2.position + new Vector2(0f, 0.05f));
            name3.MovePosition(name3.position + new Vector2(0f, 0.05f));
            name4.MovePosition(name4.position + new Vector2(0f, 0.05f));
            yield return null;
        }
        yield return new WaitForSeconds(5);
        name1.isKinematic = false;
        name2.isKinematic = false;
        name3.isKinematic = false;
        name4.isKinematic = false;
    }
}
