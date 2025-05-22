using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncerThing : MonoBehaviour
{
    public ProjectileBruh projectile;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Feet"))
        {
            Debug.Log("Feet");
            collider.gameObject.GetComponentInParent<Chara>().jumpOnJumpables();
            if (projectile != null)
            {
                projectile.canMove = false;
                projectile.animator.SetTrigger("Splash");
            }
        }
    }
}
