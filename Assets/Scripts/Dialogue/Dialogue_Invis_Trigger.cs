using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Invis_Trigger : MonoBehaviour
{
    [SerializeField] private bool isTriggeredOnce;

    // Trigerring Dialogue
    public Dialogue dialogue;

    // size of index and player names should be same
    // i.e. [2, 4, 6]
    //      [Kep, Rob, Kep]
    public int[] next_player_index;
    public string[] player_names;

    public GameObject checkpoint;

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
        if (isTriggeredOnce == false && collision.tag == "Player" && checkpoint.GetComponent<Set_Invis_bool>().can_trigger_invisible_dialogue == true)
        {
            Trigger_Dialogue();
            isTriggeredOnce = true;
        }

    }
}
