using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    // ----- Public Variables ----- 
    public bool isTrapTrigger = false;

    // ----- Private variables ----- 
    CheckpointManager manager;
    private AudioSource source;
    private bool alarmTriggered = false;

    private void Start()
    {
        manager = transform.parent.GetComponent<CheckpointManager>();
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            manager.UpdateCheckpoint(gameObject);

            if (isTrapTrigger && !alarmTriggered)
            {
                alarmTriggered = true;

                // Sound alarm 
                source.Play();

                // Activate traps
                foreach (GameObject trap in GameObject.FindGameObjectsWithTag("Trap"))
                {
                    RotationScript script = trap.GetComponent<RotationScript>();
                    script.SetActivated(true); 
                }
            }
        }
    }
}
