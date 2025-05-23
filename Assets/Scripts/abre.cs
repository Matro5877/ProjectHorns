using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class abre : MonoBehaviour
{
    public GameObject counterObject;
    // Start is called before the first frame update
    void Start()
    {
        counterObject = GameObject.Find("Scor");
        counterObject.GetComponent<AudioSource>().enabled = true;
        counterObject.GetComponent<AudioSource>().DOFade(0f, 0f);
        counterObject.GetComponent<AudioSource>().DOFade(1f, 1f);
        Debug.Log("La musique");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
