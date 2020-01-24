﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public void Respawn()
    {
        GameObject cp = CheckpointManager.Instance.GetCheckpoint();
        transform.position = cp.transform.position;
    }
}
