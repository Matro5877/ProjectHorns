using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class scre : MonoBehaviour
{
    public Chara chara;
    public GameObject charaObject;

    public int fruitCount;
    public int dynamicFruitCount;

    public int savedFruitCount;
    public int savedDynamicFruitCount;

    public GameObject dynamicFruitCountObject;
    public Animator dynamicFruitCountIcon;

    public int healthCount;

    public bool fruitBoost;

    public TMP_Text scoreText;

    public string destination;

    // Start is called before the first frame update
    void Start()
    {   
        DontDestroyOnLoad(this);
        if (destination != null)
        {
            SceneManager.LoadScene(destination);
        }
        healthCount = 5;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(fruitCount);
        DynamicFruitCountController();
        scoreText.text = $"{fruitCount}";
    }

    public void FruitAdd(int amount)
    {
        fruitCount += amount;
        dynamicFruitCount += amount;
    }

    public void SaveFruitCount()
    {
        savedFruitCount = fruitCount;
        savedDynamicFruitCount = dynamicFruitCount;
    }

    public void DynamicFruitCountController()
    {
        if (dynamicFruitCount == 6)
        {
            fruitBoost = true;
        }
        else
        {
            fruitBoost = false;
        }

        if (dynamicFruitCount > 6)
        {
            dynamicFruitCount -= 6;
            if (healthCount < 3)
            {
                healthCount ++;
            }
        }
    }
}
