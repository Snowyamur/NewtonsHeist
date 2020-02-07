using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private CheckpointManager mgrScript;

    private void Start()
    {
        mgrScript = GameObject.FindGameObjectWithTag("CheckpointManager").GetComponent<CheckpointManager>();
    }

    public void Respawn()
    {
        GameObject cp = mgrScript.GetCheckpoint();
        transform.position = cp.transform.position;  // TODO: make this more robust, e.g. include 
                                                     // world time pausing and enemy reset
    }
}
