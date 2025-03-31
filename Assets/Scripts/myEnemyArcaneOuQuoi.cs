using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myEnemyArcaneOuQuoi : MonoBehaviour
{

    public Test chara;
    public Vector2 enemyPos2D;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Core"))
        {
            Debug.Log("Enemy Touche Joueur");
            chara.getHitBySomething(enemyPos2D);
        }
    }

    private void Update()
    {
        enemyPos2D = transform.position;
    }

}
