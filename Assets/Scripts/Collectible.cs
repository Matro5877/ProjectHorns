using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public Chara chara;
    public GameObject Fruit;
    public int fruitAmount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Triggerer"))
        {
            Debug.Log("Triggerer");
            chara.AddFruits(fruitAmount);
            Fruit.SetActive(false);
        }
    }
}
