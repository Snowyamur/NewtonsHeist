using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    GravityController gravControl;

    void Start()
    {
        gravControl = GetComponent<GravityController>();
    }
    /*private CheckpointManager mgrScript;

    private void Start()
    {
        mgrScript = GameObject.FindGameObjectWithTag("CheckpointManager").GetComponent<CheckpointManager>();
    }*/

    public void Respawn()
    {
        transform.position = LevelManager.current.lastCheckpoint.transform.position;
        gravControl.gravDir = GravityController.GravityDirection.Down;
        transform.eulerAngles = new Vector3(0, 0, 0);
        //GameObject cp = mgrScript.GetCheckpoint();
        //transform.position = cp.transform.position;  // TODO: make this more robust, e.g. include
                                                     // world time pausing and enemy reset
    }
}
