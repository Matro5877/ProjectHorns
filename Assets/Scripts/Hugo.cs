using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Hugo : MonoBehaviour
{
    public bool advance;
    public float hugoSpeed;

    // Start is called before the first frame update
    void Start()
    {
        advance = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (advance)
        {
            transform.position = new UnityEngine.Vector3(transform.position.x, transform.position.y, transform.position.z - hugoSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Triggerer"))
        {
            advance = true;
        }
    }
}
