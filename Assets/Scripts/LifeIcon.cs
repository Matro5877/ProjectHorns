using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeIcon : MonoBehaviour
{
    public int id;

    public GameObject counterObject;
    public scre counter;

    public Animator animator;
    private bool isTaken;

    public bool theBigOne;

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
        if (counter.healthCount < id)
        {
            if (!isTaken)
            {
                animator.SetTrigger("anim_LifeTaken");
            }
            
            isTaken = true;
        }
        else
        {
            if (isTaken)
            {
                animator.SetTrigger("anim_LifeObtained");
            }

            isTaken= false;
        }
    }

    public void isDead()
    {

    }
}
