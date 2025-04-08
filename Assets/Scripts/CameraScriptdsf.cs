using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScriptsdsf : MonoBehaviour
{

    public float yCameraOffset;
    public float xCameraOffset;
    public float yCameraPosition;
    public float xCameraPosition;
    public GameObject Player;

    public string cameraMode;

    // Start is called before the first frame update
    void Start()
    {
        cameraMode = "fullCharacter";
        //cameraMode = "xCharacter";
        //cameraMode = "yCharacter";
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraMode == "fullCharacter")
        {
            transform.position = new Vector3(Player.transform.position.x + xCameraOffset, Player.transform.position.y + yCameraOffset, transform.position.z);
        }
        if (cameraMode == "xCharacter")
        {
            transform.position = new Vector3(Player.transform.position.x + xCameraOffset, yCameraPosition, transform.position.z);
        }
        if (cameraMode == "yCharacter")
        {
            transform.position = new Vector3(xCameraPosition, Player.transform.position.y + yCameraOffset, transform.position.z);
        }
    }
}
