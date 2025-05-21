using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public myEnemyArcaneOuQuoi enemyScript;

    public bool isShootRange;
    public bool hasSpawned;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Triggerer"))
        {
            if (!isShootRange)
            {
                enemyScript.Spawn();
                hasSpawned = true;
            }
            else
            {
                if (hasSpawned)
                {
                    enemyScript.isInRange = true; 
                    StartCoroutine(enemyScript.ShootTimer());
                }
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Triggerer"))
        {
            if (!isShootRange)
            {
                //rien
            }
            else
            {
                enemyScript.isInRange = false;
            }
        }
    }
}
