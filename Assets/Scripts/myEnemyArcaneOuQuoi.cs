using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myEnemyArcaneOuQuoi : MonoBehaviour
{
    public bool isProjectile;
    public bool canHit;
    public float stunTime = 0.2f;
    public float shootTimer;

    public Vector2 enemyPos2D;
    public Vector2 direction;
    public bool isFlipped;
    public bool isInRange;
    public bool isInSpawnRange;
    public bool isWaiting;

    public Chara chara;
    public GameObject charaObject;

    public GameObject projectilePrefab;
    
    public SpriteRenderer spriteRenderer;
    public Collider2D collider;
    public Animator animator;

    void Start()
    {
        spriteRenderer.enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Core"))
        {
            if (canHit)
            {
                animator.SetBool("isPushing", true);
                Debug.Log("Enemy Touche Joueur");
                StartCoroutine(chara.Stun(stunTime));
                chara.getHitBySomething(enemyPos2D);
            }
        }
    }

    private void Update()
    {
        if (!isProjectile)
        {
            //If the enemy isn't a Projectile
           TurnManager();
            if (Input.GetKeyDown(KeyCode.F))
            {
                Shoot();
            } 
            spriteRenderer.flipX = isFlipped;
        }
        else
        {
            //If the enemy is a Projectile
            
        }
        
        enemyPos2D = transform.position; 
    }

    public void TurnManager()
    {
        if ((charaObject.transform.position.x > transform.position.x && !isFlipped) || (charaObject.transform.position.x < transform.position.x && isFlipped))
        {
            Turn();
        }
    }

    public void Turn()
    {
        animator.SetTrigger("Turn");
        Debug.Log("TurnEnd");
        isFlipped = !isFlipped;
    }

    public void Shoot()
    {
        animator.SetTrigger("Shoot");
    }

    public void Spawn()
    {
        spriteRenderer.enabled = true;
        animator.SetTrigger("Spawn");
        StartCoroutine(ShootTimer());
    }

    public void Die()
    {
        animator.SetTrigger("Die");
    }

    public void Dissapear()
    {
        spriteRenderer.enabled = false;
        collider.enabled = false;

    }

    public IEnumerator ShootTimer()
    {
        isWaiting= true;
        yield return new WaitForSeconds(shootTimer);

        if (isInRange)
        {
            Debug.Log("Shoot");

            isWaiting = false;

            Shoot();
            StartCoroutine(ShootTimer());
        }
    }
}
