using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncerThing : MonoBehaviour
{

    public Chara chara;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Feet"))
        {
            Debug.Log("Feet");
            chara.jumpOnJumpables();
        }
    }
}
