using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GravityThrowable : Throwable
{
    void Start()
    {
        name = "Gravity Manipulator";
        id = 1;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "SpiderDrone")
        {
            collision.gameObject.GetComponent<SpiderDrone>().SwitchGravity();
        }
        else if(collision.transform.tag == "FlyingDrone")
        {
            collision.gameObject.GetComponent<SpiderDrone>().SwitchGravity();
        }
        Destroy(gameObject);
    }
}
