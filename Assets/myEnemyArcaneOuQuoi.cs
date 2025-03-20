using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myEnemyArcaneOuQuoi : MonoBehaviour
{

    public Test chara;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Core"))
        {
            chara.getHitBySomething();
        }

        if (collider.CompareTag("Feet"))
        {
            Debug.Log("Feet");
            chara.jumpOnJumpables();
        }
    }

}
