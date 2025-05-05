using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FancyCamera : MonoBehaviour
{
    public float yCameraOffset;
    public float xCameraOffset;
    public float yCameraPosition;
    public float xCameraPosition;

    public Vector3 CamPos;
    public bool stopCamera;

    public GameObject Player;

    void Start()
    {
        
    }

    void Update()
    {
        CamPos = transform.position;
        transform.position = new Vector3(Player.transform.position.x + xCameraOffset, yCameraPosition, transform.position.z);
        if (stopCamera)
        {
            Debug.Log("Roblox");
            transform.position = CamPos;
            stopCamera = false;
        }
    }

    public void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("AntiCamera"))
        {
            Debug.Log("Fortnite");
            stopCamera = true;
        }
    }
}
