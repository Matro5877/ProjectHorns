using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using DG.Tweening;
using static Unity.Collections.AllocatorManager;

public class TitleScreenController : MonoBehaviour
{
    public SpriteRenderer controllerScreen;
    public SpriteRenderer languageScreen;
    public SpriteRenderer titleScreen;
    public SpriteRenderer languageSelect;
    public SpriteRenderer englishSelect;
    public SpriteRenderer frenchSelect;
    public SpriteRenderer square;

    void Start()
    {
        UnityEngine.Color white = new UnityEngine.Color(1f, 1f, 1f, 1f);
        UnityEngine.Color black = new UnityEngine.Color(0f, 0f, 0f, 1f);
        UnityEngine.Color transparentWhite = new UnityEngine.Color(1f, 1f, 1f, 0f);
        UnityEngine.Color transparentBlack = new UnityEngine.Color(0f, 0f, 0f, 0f);

        StartCoroutine(ControlScreen());
    }

    IEnumerator ControlScreen()
    {
        UnityEngine.Color white = new UnityEngine.Color(1f, 1f, 1f, 1f);
        UnityEngine.Color black = new UnityEngine.Color(0f, 0f, 0f, 1f);
        UnityEngine.Color transparentWhite = new UnityEngine.Color(1f, 1f, 1f, 0f);
        UnityEngine.Color transparentBlack = new UnityEngine.Color(0f, 0f, 0f, 0f);

        square.color = black;
        square.material.DOColor(transparentBlack, 2f);

        yield return new WaitForSeconds(4);

        square.material.DOColor(black, 2f);

        yield return new WaitForSeconds(3);

        controllerScreen.color = transparentWhite;
        languageScreen.color = white;
        square.material.DOColor(transparentBlack, 2f);
    }

    void Update()
    {
        
    }
}
