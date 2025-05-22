using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCounterIcon : MonoBehaviour
{
    public GameObject counterObject;
    public scre counter;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        counterObject = GameObject.Find("Scor");
        if (counterObject != null)
        {
            counter = counterObject.GetComponent<scre>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("anim_dynamicFruitCount", counter.dynamicFruitCount);
    }
}
