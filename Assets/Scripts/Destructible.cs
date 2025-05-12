using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public int hp;
    public Chara chara;
    public Vector2 destructiblePos2D;
    public Collider2D destructibleCollider2D;
    public SpriteRenderer destructibleSpriteRenderer;
    public float destructibleRespawnTime;
    public bool canBeDestroyed;
    public float stunTime = 0.4f;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Dash"))
        {
            if (canBeDestroyed)
            {
                Debug.Log("Destructible Touche Joueur");
                StartCoroutine(chara.Stun(stunTime));
                chara.getHitBySomething(destructiblePos2D);
                destructibleCollider2D.enabled = false;
                destructibleSpriteRenderer.enabled = false;
                StartCoroutine(DestructibleRespawn());
            }
        }
    }

    public IEnumerator DestructibleRespawn()
    {
        yield return new WaitForSeconds(destructibleRespawnTime);

        destructibleCollider2D.enabled = true;
        destructibleSpriteRenderer.enabled = true;
    }
}
