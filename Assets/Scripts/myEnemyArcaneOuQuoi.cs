using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myEnemyArcaneOuQuoi : MonoBehaviour
{
    public bool isRed;
    public bool isProjectile;
    public bool canHit;
    public bool doesDirectShoot;
    public float stunTime = 0.2f;
    public float shootTimer;
    public float yProjectileOffset;
    public float xProjectileOffset;

    public Vector2 enemyPos2D;
    public Vector2 direction;
    public bool isFlipped;
    public bool isInRange;
    public bool isInSpawnRange;
    public bool isWaiting;
    public bool fruitIsGiven;

    public Chara chara;
    public GameObject charaObject;

    public GameObject projectilePrefabRight;
    public GameObject projectilePrefabLeft;

    public GameObject fruitGift;
    public GameObject fruitKillAnim;
    public Animator fruitKillAnimator;
    
    public SpriteRenderer spriteRenderer;
    public Collider2D collider;
    public Animator animator;

    void Start()
    {
        fruitIsGiven = false;
        if (!isProjectile)
        {
            spriteRenderer.enabled = false;
        }
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

        if (fruitIsGiven)
        {
            FruitFollow();
        }
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

    public void DirectShoot()
    {
        if (doesDirectShoot)
        {
            Shoot();
        }
    }

    public void SpawnProjectile()
    {
        if (isFlipped)
        {
            Instantiate(projectilePrefabRight, new Vector2(transform.position.x + xProjectileOffset, transform.position.y + yProjectileOffset), transform.rotation);
        }
        else
        {
            Instantiate(projectilePrefabLeft, new Vector2(transform.position.x - xProjectileOffset, transform.position.y + yProjectileOffset), transform.rotation);
        }
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
        fruitKillAnimator.SetTrigger("Kill");
        fruitIsGiven = true;
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
        isWaiting = false;
        if (isInRange)
        {
            Debug.Log("Shoot");

            

            Shoot();
            StartCoroutine(ShootTimer());
        }
    }

    public void FruitFollow()
    {
        fruitGift.transform.position = fruitKillAnim.transform.position;
    }
}
