using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlyingDroneAnimController : EnemyAnimController
{
    FlyingDrone mechanics;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        mechanics = GetComponent<FlyingDrone>();
    }

    void Update()
    {
        anim.SetBool("isWalking", mechanics.isWalking);
        anim.SetBool("isIdle", mechanics.isIdle);
        anim.SetBool("isTurning", mechanics.isTurning);
    }
}
