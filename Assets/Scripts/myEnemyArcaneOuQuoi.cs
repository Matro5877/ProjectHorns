using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myEnemyArcaneOuQuoi : MonoBehaviour
{

    public Chara chara;
    public Vector2 enemyPos2D;
    public bool canHit;
    public float stunTime = 0.2f;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Core"))
        {
            if (canHit)
            {
                Debug.Log("Enemy Touche Joueur");
                StartCoroutine(chara.Stun(stunTime));
                chara.getHitBySomething(enemyPos2D);
            }
        }
    }

    private void Update()
    {
        enemyPos2D = transform.position;
    }
}
