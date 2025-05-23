using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RemoverDDOL : MonoBehaviour
{
    public bool isRemovingCounter;
    public bool isChangingMusic;

    public string targetSceneName;
    public GameObject itSelf;

    private string currentSceneName;

    public GameObject counterObject;
    public GameObject canvasObject;

    public AudioSource source;

    void Start()
    {
        if (isRemovingCounter)
        {
            counterObject = GameObject.Find("Scor");
            Destroy(counterObject);
            canvasObject = GameObject.Find("FruitCanvas");
            Destroy(canvasObject);
            source.Stop();
            source.Play();
        }
        else
        {
            counterObject = GameObject.Find("Scor");
            counterObject.GetComponent<AudioSource>().enabled = false;
            source.Stop();
            source.Play();
        }
    }
}
