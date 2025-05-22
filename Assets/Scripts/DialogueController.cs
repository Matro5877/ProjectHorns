using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DialogueController : MonoBehaviour
{
    public SpriteRenderer dialogueBox;
    public GameObject npc;
    public Transform dialogueBoxObject;

    public bool isNpc;
    public bool showDialogue;

    UnityEngine.Color white = new UnityEngine.Color(1f, 1f, 1f, 1f);
    UnityEngine.Color black = new UnityEngine.Color(0f, 0f, 0f, 1f);
    UnityEngine.Color transparentWhite = new UnityEngine.Color(1f, 1f, 1f, 0f);
    UnityEngine.Color transparentBlack = new UnityEngine.Color(0f, 0f, 0f, 0f);

    public Vector3 scaleSaver;

    void Awake()
    {
        scaleSaver = transform.localScale;
    }
    
    void Start()
    {
        //Debug.Log(this.name + scaleSaver);
        //dialogueBox.material.DOColor(transparentWhite, 0f);
        dialogueBoxObject.DOMove(new Vector3(npc.transform.position.x + 0, npc.transform.position.y + 0.5f, transform.position.z), 0f);
        dialogueBoxObject.DOScale(new Vector3(0, 0, 0), 0f);
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Triggerer") && !isNpc)
        {
            Debug.Log("In");
            showDialogue = true;

            dialogueBoxObject.DOMove(new Vector3(npc.transform.position.x + 0, npc.transform.position.y + 1, transform.position.z), 1f);
            dialogueBoxObject.DOScale(scaleSaver, 1f);
            //dialogueBox.material.DOColor(white, 1f);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Triggerer") && !isNpc)
        {
            Debug.Log("Out");
            showDialogue = false;

            dialogueBoxObject.DOMove(new Vector3(npc.transform.position.x + 0, npc.transform.position.y + 0.5f, transform.position.z), 1f);
            dialogueBoxObject.DOScale(new Vector3(0, 0, 0), 1f);
            //dialogueBox.material.DOColor(transparentWhite, 1f);
        }
    }
}
