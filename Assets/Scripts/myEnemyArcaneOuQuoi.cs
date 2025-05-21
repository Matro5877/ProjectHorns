using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myEnemyArcaneOuQuoi : MonoBehaviour
{

    public Chara chara;
    public GameObject charaObject;
    public Vector2 enemyPos2D;
    public bool canHit;
    public float stunTime = 0.2f;
    public Vector2 direction;
    public bool isFlipped;
    public bool isInRange;

    public float shootTimer;

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
        TurnManager();
        if (Input.GetKeyDown(KeyCode.F))
        {
            Die();
        }
        enemyPos2D = transform.position;
        spriteRenderer.flipX = isFlipped;
        Debug.Log(isFlipped);
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
        yield return new WaitForSeconds(shootTimer);

        if (isInRange)
        {
            Shoot();
            Debug.Log("Shoot");
            StartCoroutine(ShootTimer());
        }
    }
}
