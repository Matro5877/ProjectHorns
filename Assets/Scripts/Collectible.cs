using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public Chara chara;
    public GameObject Fruit;
    public int fruitAmount;

    public float currentSpeed;
    public float acceleration;
    public float speedLimit;

    public bool isAuto;

    // Start is called before the first frame update
    void Start()
    {
        
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
            chara.AddFruits(fruitAmount);
            Fruit.SetActive(false);
        }
        else if (collider.CompareTag("Core") && !isAuto)
        {
            chara.AddFruits(fruitAmount);
            Fruit.SetActive(false);
        }
    }
}
