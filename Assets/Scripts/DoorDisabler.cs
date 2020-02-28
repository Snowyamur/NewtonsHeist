using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDisabler : MonoBehaviour
{
    GameObject door;

    void Start()
    {
        door = GameObject.Find("Machine Door");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "EMP")
        {
            door.SetActive(false);
        }
    }
}
