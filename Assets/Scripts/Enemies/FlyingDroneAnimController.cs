using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlyingDroneAnimController : EnemyAnimController
{
    Animator anim;
    FlyingDrone mechanics;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        mechanics = GetComponent<FlyingDrone>();
    }

}
