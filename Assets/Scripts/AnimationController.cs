using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator anim;
    PlayerMechanics mechanics;
    SpriteRenderer playerSprite;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        mechanics = GetComponent<PlayerMechanics>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    //Updates animations based on current movement
    void Update()
    {
        anim.SetBool("isWalking", mechanics.isWalking);
        anim.SetBool("isCrouching", mechanics.isCrouching);
        anim.SetBool("isJumping", mechanics.isJumping);
        anim.SetBool("isFalling", mechanics.isFalling);
        anim.SetBool("isIdle", mechanics.isIdle);
    }
}
