using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Trigger : MonoBehaviour
{
    [SerializeField] private bool isTriggeredOnce;
    // Trigerring Dialogue
    public Dialogue dialogue;

    private void Start()
    {
        isTriggeredOnce = false;
    }
    public void Trigger_Dialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTriggeredOnce == false)
        {
            Trigger_Dialogue();
            isTriggeredOnce = true;
        }
        
    }

}
