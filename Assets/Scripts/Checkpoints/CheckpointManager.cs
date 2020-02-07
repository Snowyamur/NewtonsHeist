﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    // ----- Private variables ----- 
    List<GameObject> checkpoints;
    int currIndex;

    // Start is called before the first frame update
    private void Start()
    {
        checkpoints = new List<GameObject>();

        for (int i = 0; i < transform.childCount; ++i)
        {
            checkpoints.Add(transform.GetChild(i).gameObject);
        }
    }

    public bool UpdateCheckpoint(GameObject newCP)
    {
        try
        {
            currIndex = checkpoints.IndexOf(newCP);

            LevelManager.current.lastCheckpoint = checkpoints[currIndex];
            return true;
        }
        catch
        {
            return false;
        }
    }

    public GameObject GetCheckpoint()
    {
        return checkpoints[currIndex];
    }
}
