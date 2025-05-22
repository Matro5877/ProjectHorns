using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectible : MonoBehaviour
{
    public Chara chara;
    public GameObject Fruit;
    public int fruitAmount;

    public GameObject counterObject;
    public scre counter;

    public float currentSpeed;
    public float acceleration;
    public float speedLimit;

    public bool isAuto;

    public UnityEvent addFruitToCounter;

    // Start is called before the first frame update
    void Start()
    {
        counterObject = GameObject.Find("Scor");
        if (counterObject != null)
        {
            counter = counterObject.GetComponent<scre>();
        }
        //Debug.Log($"Counter = {counter != null}");
    }

    // Update is called once per frame
    void Update()
    {
        if (isAuto)
        {
            transform.position = new Vector2(transform.position.x,transform.position.y + currentSpeed);
            currentSpeed += acceleration * Time.deltaTime;
            if (currentSpeed > speedLimit)
            {
                acceleration = - acceleration;
            }
            if (currentSpeed < - speedLimit)
            {
                acceleration = - acceleration;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Triggerer") && isAuto)
        {
            //Debug.Log("Triggerer");
            //chara.AddFruits(fruitAmount);
            counter.FruitAdd(fruitAmount);
            Fruit.SetActive(false);
        }
        else if (collider.CompareTag("Core") && !isAuto)
        {
            //chara.AddFruits(fruitAmount);
            counter.FruitAdd(fruitAmount);
            Fruit.SetActive(false);
        }
    }
}
