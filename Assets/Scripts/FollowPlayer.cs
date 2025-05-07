using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject chara;

    public float xOffset;
    public float yOffset;

    void Update()
    {
        transform.position = new Vector2(chara.transform.position.x + xOffset,chara.transform.position.y + yOffset);
    }
}
