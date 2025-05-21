using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public myEnemyArcaneOuQuoi enemyScript;
    public EnemyTrigger otherTrigger;

    public bool isShootRange;
    public bool hasSpawned;
    public bool isInRange;
    public bool isInSpawnRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Is In Range");
        if (isInRange && !enemyScript.isWaiting && hasSpawned)
        {
            StartCoroutine(enemyScript.ShootTimer());
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Triggerer"))
        {
            if (!isShootRange)
            {
                //For the Spawn Trigger
                enemyScript.Spawn();
                if (!hasSpawned)
                {
                    Debug.Log("First Spawn");
                }
                otherTrigger.hasSpawned = true;
                hasSpawned = true;
            }
            else
            {
                enemyScript.Shoot();
                //For the Range Trigger
                isInRange = true;
                enemyScript.isInRange = true;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Triggerer"))
        {
            if (!isShootRange)
            {
                //For the Spawn Trigger
            }
            else
            {
                //For the Range Trigger
                isInRange = false;
                enemyScript.isInRange = false;
            }
        }
    }
}
