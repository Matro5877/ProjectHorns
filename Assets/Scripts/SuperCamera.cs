using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperCamera : MonoBehaviour
{
    public float yCameraOffset = 0f;
    public float xCameraOffset = 0f;
    public float yCameraPosition;
    public GameObject Cam;
  

    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        Cam.transform.position = new Vector3(transform.position.x + xCameraOffset, transform.position.y + yCameraOffset, transform.position.z);
    }
}
