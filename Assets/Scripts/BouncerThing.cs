using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncerThing : MonoBehaviour
{
    public ProjectileBruh projectile;
    public Collider2D selfCollider;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Feet"))
        {
            Debug.Log("Feet");
            collider.gameObject.GetComponentInParent<Chara>().jumpOnJumpables();
            selfCollider.enabled = false;
            projectile.canHit = false;
            if (projectile != null)
            {
                projectile.canMove = false;
                projectile.animator.SetTrigger("Splash");
            }
        }
    }
}
