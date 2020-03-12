using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach this script to a GameObject to rotate around the target position.
public class RotateAroundObject : MonoBehaviour
{
    public GameObject target;
    public float angle;

    void Update()
    {
        // Spin the object around the target's origin at angle degrees/second.
        transform.RotateAround(target.transform.position, Vector3.forward, angle * Time.deltaTime);
    }
}
