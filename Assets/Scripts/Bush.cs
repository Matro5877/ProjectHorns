using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    public GameObject bushParticles;
    public Chara chara;
    
    // Start is called before the first frame update
    void Start()
    {
        bushParticles.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Triggerer"))
        {
            bushParticles.SetActive(true);
            chara.forcedRight = true;
            chara.keysEnabled = false;
        }
    }
}
