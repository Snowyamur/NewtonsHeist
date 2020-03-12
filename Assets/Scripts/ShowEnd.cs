using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEnd : MonoBehaviour
{
    GameObject goal;

    void Start()
    {
        goal = GameObject.FindGameObjectWithTag("Goal");
        goal.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.tag == "Goal")
        {
            goal.SetActive(true);
        }
    }
}
