using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RemoverDDOL : MonoBehaviour
{
    public string targetSceneName;
    public GameObject itSelf;

    private string currentSceneName;

    public GameObject counterObject;
    public GameObject canvasObject;

    public AudioSource source;

    void Start()
    {
        counterObject = GameObject.Find("Scor");
        Destroy(counterObject);
        canvasObject = GameObject.Find("FruitCanvas");
        Destroy(canvasObject);
        source.Stop();
        source.Play();
    }
}
