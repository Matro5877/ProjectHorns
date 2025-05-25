using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class scre : MonoBehaviour
{
    public SpriteRenderer charaSprite;
    public SpriteRenderer charaDeathSprite;
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
        healthCount = 4;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(fruitCount);
        DynamicFruitCountController();
        scoreText.text = $"{fruitCount}";

        if (Input.GetKeyDown(KeyCode.O))
        {
            healthCount--;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            healthCount++;
        }

        if (healthCount < 1)
        {
            charaObject = GameObject.Find("Chara");
            if (charaObject != null)
            {
                chara = charaObject.GetComponent<Chara>();
                chara.isDead = true;
                charaSprite = GameObject.Find("Chara").GetComponent<SpriteRenderer>();
                charaDeathSprite = GameObject.Find("CharaDeath").GetComponent<SpriteRenderer>();
                charaSprite.enabled = false;
                charaDeathSprite.enabled = true;

            }
        }
        if (healthCount > 4)
        {
            healthCount = 4;
        }
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

    public void LoadFruitCount()
    {
        fruitCount = savedFruitCount;
        dynamicFruitCount = 0;
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
