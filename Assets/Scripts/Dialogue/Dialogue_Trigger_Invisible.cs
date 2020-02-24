using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Trigger_Invisible : MonoBehaviour
{
    [SerializeField] private bool isTriggeredOnce;
    
    // Trigerring Dialogue
    public Dialogue dialogue;

    // size of index and player names should be same
    // i.e. [2, 4, 6]
    //      [Kep, Rob, Kep]
    [SerializeField] private GameObject player;
    public int[] next_player_index;
    public string[] player_names;

    private void Start()
    {
        isTriggeredOnce = false;
    }
    public void Trigger_Dialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, next_player_index, player_names, dialogue.sentences);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (isTriggeredOnce == false && collision.tag == "Player" && player.GetComponent<PlayerMechanics>().canTriggerInvisibleDialogue == true)
        {
            Trigger_Dialogue();
            isTriggeredOnce = true;
        }
        
    }

}
