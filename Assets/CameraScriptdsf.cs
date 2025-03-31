using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScriptsdsf : MonoBehaviour
{

    public float offset;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Player.transform.position.x + offset,Player.transform.position.y,transform.position.z);
    }
}
