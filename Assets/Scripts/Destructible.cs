using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public int hp;
    public Chara chara;
    public Vector2 destructiblePos2D;
    public float destructibleRespawnTime;
    public bool canBeDestroyed;
    public myEnemyArcaneOuQuoi enemy;
    public bool canBeKilled;
    public float stunTime = 0.4f;

    public Collider2D selfCollider2D;

    public bool isSecret;

    void Start()
    {
        
    }

    void Update()
    {
        destructiblePos2D = transform.position;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Dash"))
        {
            if (canBeDestroyed)
            {
                //Debug.Log("Destructible Touche Joueur");
                StartCoroutine(chara.Stun(stunTime));
                chara.getHitBySomething(destructiblePos2D);
                /*foreach (SpriteRenderer child in transform)
                {
                    child.enabled = false;
                    Debug.Log("child");
                }
                foreach (Collider2D child in transform)
                {
                    child.enabled = false;
                }*/
                hp --;
                if (hp < 1)
                {
                    foreach (Transform child in transform)
                {
                    SpriteRenderer childSpriteRenderer = child.GetComponent<SpriteRenderer>();
                    if (childSpriteRenderer != null)
                    {
                        childSpriteRenderer.enabled = false;
                    }
                    Collider2D childCollider2D = child.GetComponent<Collider2D>();
                    if (childCollider2D != null)
                    {
                        childCollider2D.enabled = false;
                    }
                }
                selfCollider2D.enabled = false;
                }
                
                
                //destructibleCollider2D.enabled = false;
                //destructibleSpriteRenderer.enabled = false;
                if (destructibleRespawnTime > 0)
                {
                    StartCoroutine(DestructibleRespawn());
                }
                /*
                Debug.Log("Destructible Touche Joueur");
                StartCoroutine(chara.Stun(stunTime));
                chara.getHitBySomething(destructiblePos2D);
                destructibleObject.SetActive(false);
                */
            }
            else if (canBeKilled)
            {
                hp --;
                
                if (hp < 1)
                {
                    enemy.Die();
                }
            }
        }
    }

    public IEnumerator DestructibleRespawn()
    {
        yield return new WaitForSeconds(destructibleRespawnTime);

        foreach (Transform child in transform)
        {
            SpriteRenderer childSpriteRenderer = child.GetComponent<SpriteRenderer>();
            if (childSpriteRenderer != null)
            {
                childSpriteRenderer.enabled = true;
            }
            Collider2D childCollider2D = child.GetComponent<Collider2D>();
            if (childCollider2D != null)
            {
                childCollider2D.enabled = true;
            }
        }
    }
}
