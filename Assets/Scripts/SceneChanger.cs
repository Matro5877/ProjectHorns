using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneChanger : MonoBehaviour
{
    public bool fadeIn;

    public SpriteRenderer transition;

    UnityEngine.Color white = new UnityEngine.Color(1f, 1f, 1f, 1f);
    UnityEngine.Color black = new UnityEngine.Color(0f, 0f, 0f, 1f);
    UnityEngine.Color transparentWhite = new UnityEngine.Color(1f, 1f, 1f, 0f);
    UnityEngine.Color transparentBlack = new UnityEngine.Color(0f, 0f, 0f, 0f);

    void Start()
    {
        if (!fadeIn)
        {
            transition.color = black;
            transition.material.DOColor(transparentBlack, 0f);
        }
        else
        {
            transition.color = black;
            transition.material.DOColor(black, 0f);
            transition.material.DOColor(transparentBlack, 1f);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Triggerer") && !fadeIn)
        {
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        transition.material.DOColor(black, 1f);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("TestRoom02");
    }


}
