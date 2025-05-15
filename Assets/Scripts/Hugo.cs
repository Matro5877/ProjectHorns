using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Hugo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new UnityEngine.Vector3 (transform.position.x, transform.position.y, transform.position.z - 1);
    }
}
