using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Invisibile_Dialogue : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        player.GetComponent<PlayerMechanics>().canTriggerInvisibleDialogue = true;
    }
}
