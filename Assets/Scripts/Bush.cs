using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bush : MonoBehaviour
{
    public GameObject bushParticles;
    public Chara chara;

    public bool isExit;
    public bool doesCutMusic;
    private bool used;
    public AudioSource source;
    
    // Start is called before the first frame update
    void Start()
    {
        source = GameObject.Find("Scor").GetComponent<AudioSource>();

        bushParticles.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Core"))
        {
            bushParticles.SetActive(false);
            if (isExit)
            {
                bushParticles.SetActive(true);
                if (doesCutMusic)
                {
                    source.DOFade(0f, 1f);
                }
            }
            if (!used)
            {
                chara.forcedRight = true;
                chara.keysEnabled = false;
            }
            used = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("BushExit"))
        {
            bushParticles.SetActive(false);
            if (!isExit)
            {
                bushParticles.SetActive(true);
            }
        }
        if (collider.CompareTag("Core"))
        {
            chara.forcedRight = false;
            chara.keysEnabled = true;
        }
        
    }
}
