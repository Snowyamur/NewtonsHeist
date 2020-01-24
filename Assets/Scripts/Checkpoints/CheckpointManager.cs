using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    // ----- Private variables ----- 
    List<GameObject> checkpoints;
    int currIndex;
    static CheckpointManager instance = null;

    // Start is called before the first frame update
    private void Start()
    {
        instance = this;
        checkpoints = new List<GameObject>();

        for (int i = 0; i < transform.childCount; ++i)
        {
            checkpoints.Add(transform.GetChild(i).gameObject);
        }
    }

    public static CheckpointManager Instance { get { return instance; } }

    public bool UpdateCheckpoint(GameObject newCP)
    {
        Debug.Log("updating checkpoint... " + newCP);
        try
        {
            currIndex = checkpoints.IndexOf(newCP);
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
