using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyAnimController : MonoBehaviour
{
    protected Animator anim;
    EnemyAI mechanics;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        mechanics = GetComponent<EnemyAI>();
    }

    //Updates animations based on current movement
    void Update()
    {
        anim.SetBool("isWalking", mechanics.isWalking);
        anim.SetBool("isIdle", mechanics.isIdle);
        anim.SetBool("isTurning", mechanics.isTurning);
    }
}
