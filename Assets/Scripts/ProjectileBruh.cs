using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBruh : MonoBehaviour
{
    public Chara chara;

    public SpriteRenderer spriteRenderer;
    public Collider2D selfCollider;
    public Animator animator;
    public GameObject projectile;

    public bool canHit;
    public bool isMovingRight;

    public int damage;
    public float speed;
    public float stunTime;

    public bool canMove;

    void Start()
    {
        canMove = true;
        selfCollider.enabled = true;
    }

    void Update()
    {
        Move();

        spriteRenderer.flipX = isMovingRight;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Core"))
        {
            if (canHit)
            {
                Debug.Log("Enemy Touche Joueur");

                canMove = false;
                selfCollider.enabled = false;
                canHit = false;

                animator.SetTrigger("Splash");

                StartCoroutine(collider.gameObject.GetComponentInParent<Chara>().Stun(stunTime));
                collider.gameObject.GetComponentInParent<Chara>().getHitBySomething(transform.position);
            }
        }
        if (collider.CompareTag("Solid Platform"))
        {
            canMove = false;

            animator.SetTrigger("Splash");
        }
    }

    public void Move()
    {
        if (canMove)
        {
            if (isMovingRight)
            {
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            }
        }
    }

    public void DestroyProjectile()
    {
        //projectile.SetActive(false);
        Destroy(projectile);
    }
}
