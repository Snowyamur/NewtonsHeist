using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    // ----- Private variables ----- 
    CheckpointManager manager; 

    private void Start()
    {
        manager = transform.parent.GetComponent<CheckpointManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            manager.UpdateCheckpoint(gameObject);
        }
    }
}
