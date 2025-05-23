using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
public GameObject amigo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            NewGame();
        }
    }

    public void CutAmigo()
    {
        amigo.SetActive(false);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("LoadScene");
        Debug.Log("Fini");
    }
}
