using System.Collections;
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

        if (checkpoints.Count != 0)
            LevelManager.current.lastCheckpoint = 0;
    }

    public bool UpdateCheckpoint(GameObject newCP)
    {
        try
        {
            currIndex = checkpoints.IndexOf(newCP);

            LevelManager.current.lastCheckpoint = currIndex;
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

    public GameObject GetIndexedCheckpoint(int index)
    {
        return checkpoints[index];
    }
}
