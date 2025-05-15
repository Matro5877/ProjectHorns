using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scre : MonoBehaviour
{
    public int fruitCount;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(fruitCount);
    }

    public void FruitAdd()
    {
        fruitCount ++;
    }
}
