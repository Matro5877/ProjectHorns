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

    public GameObject destructibleObject;

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
                Debug.Log("Destructible Touche Joueur");
                StartCoroutine(chara.Stun(stunTime));
                chara.getHitBySomething(destructiblePos2D);
                foreach (SpriteRenderer child in transform)
                {
                    child.enabled = false;
                    Debug.Log("child");
                }
                foreach (Collider2D child in transform)
                {
                    child.enabled = false;
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
        }
    }

    public IEnumerator DestructibleRespawn()
    {
        yield return new WaitForSeconds(destructibleRespawnTime);

        destructibleCollider2D.enabled = true;
        destructibleSpriteRenderer.enabled = true;
    }
}
