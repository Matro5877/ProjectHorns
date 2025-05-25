using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneChanger : MonoBehaviour
{
    public bool isSaving;

    public SpriteRenderer charaSprite;
    public SpriteRenderer charaDeathSprite;

    public bool fadeIn;
    public string destination;

    public GameObject transitionObject;
    public SpriteRenderer transition;

    public GameObject counterObject;
    public scre counter;

    UnityEngine.Color white = new UnityEngine.Color(1f, 1f, 1f, 1f);
    UnityEngine.Color black = new UnityEngine.Color(0f, 0f, 0f, 1f);
    UnityEngine.Color transparentWhite = new UnityEngine.Color(1f, 1f, 1f, 0f);
    UnityEngine.Color transparentBlack = new UnityEngine.Color(0f, 0f, 0f, 0f);

    void Start()
    {
        if (!fadeIn)
        {
            transitionObject.SetActive(true);
            transition.color = black;
            transition.material.DOColor(transparentBlack, 0f);
            
        }
        else
        {
            StartCoroutine(FadeIn());
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Triggerer") && !fadeIn)
        {
            counterObject = GameObject.Find("Scor");
            if (counterObject != null)
            {
                counter = counterObject.GetComponent<scre>();
            }
            charaSprite.enabled = true;
            charaDeathSprite.enabled = false;
            counter.healthCount = 4;
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeIn()
    {
        transition.color = black;
        transition.material.DOColor(black, 0f);

        yield return new WaitForSeconds(0f);

        transition.material.DOColor(transparentBlack, 1f);
    }

    IEnumerator FadeOut()
    {
        transitionObject.SetActive(true);
        transition.material.DOColor(black, 1f);

        yield return new WaitForSeconds(1f);

        counterObject = GameObject.Find("Scor");
        if (counterObject != null)
        {
            counter = counterObject.GetComponent<scre>();
        }
        
        if (isSaving)
        {
            counter.SaveFruitCount();
        }
        else
        {
            counter.LoadFruitCount();
        }
        SceneManager.LoadScene(destination);
    }


}
