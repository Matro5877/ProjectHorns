using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMadeParticle : MonoBehaviour
{
    public GameObject particle;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Chara chara;
    public Vector2 direction;
    public bool reverse;

    void Awake()
    {
        //chara = GetComponent<Chara>();
        //direction = chara.direction;
        //Debug.Log($"chara.direction: {chara.direction.x}");
        //Debug.Log($"direction: {direction.x}");
        spriteRenderer.flipX = reverse;
    }

    void Update()
    {
        //Debug.Log($"chara.direction: {chara.direction.x}");
        spriteRenderer.flipX = reverse;
    }

    public void EndParticle()
    {
        Destroy(particle);
    }
}
