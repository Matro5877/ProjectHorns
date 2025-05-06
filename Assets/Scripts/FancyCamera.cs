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
    public int stopCameraNumber;
    public bool stopCamera;
    public bool isACamera;

    public GameObject Player;
    public FancyCamera CameraCollision;
    public FancyCamera Camera;

    void Start()
    {
        
    }

    void Update()
    {
        Debug.Log($"StopCameraNumber: {stopCameraNumber}");

        if (isACamera)
        {
            if (!stopCamera)
            {
                transform.position = new Vector3(Player.transform.position.x + xCameraOffset, yCameraPosition, transform.position.z);
            }
            else
            {
                //stopCamera = false;
            }
        }
        else
        {
            transform.position = new Vector3(Player.transform.position.x + xCameraOffset, yCameraPosition, transform.position.z);
            if (stopCameraNumber > 0)
            {
                Camera.StopTheCamera();
                stopCameraNumber = 0;
            }
            else
            {
                Camera.UnstopTheCamera();
            }
        }
    }

    public void OnTriggerStay2D(Collider2D collider)
    {
        if (!isACamera)
        {
            if (collider.CompareTag("AntiCamera"))
            {
                Debug.Log("Fortnite");
                stopCameraNumber += 1;
            }
        }
    }

    public void StopTheCamera()
    {
        stopCamera = true;
    }
    public void UnstopTheCamera()
    {
        stopCamera = false;
    }
}
