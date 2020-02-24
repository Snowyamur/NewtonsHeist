using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderDroneAnimController : EnemyAnimController
{
    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        mechanics = GetComponent<SpiderDrone>();
    }

    // Update is called once per frame
    private void Update()
    {
        anim.SetBool("isWalking", mechanics.isWalking);
        anim.SetBool("isIdle", mechanics.isIdle);
        anim.SetBool("isTurning", mechanics.isTurning);
    }
}
