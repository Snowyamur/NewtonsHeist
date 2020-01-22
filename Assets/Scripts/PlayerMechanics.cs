using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMechanics : MonoBehaviour
{
    [Header("Vertical Movement")]
    [SerializeField] float JumpSpeed = 200f;
    [SerializeField] float smallJumpMod = 2f; //For double jump
    [SerializeField] float fallingMod = 2.5f; //Speed of falling
    [SerializeField] bool inAir = false;

    [Space]

    [Header("Horizontal Movement")]
    [SerializeField] float normalSpeed = 15f;
    [SerializeField] float crouchingMod = 0.5f;
    [SerializeField] float currentSpeed;
    [SerializeField] bool crouching = false;

    [Space]

    [Header("Booleans")]
    public bool isWalking;
    public bool isJumping;
    public bool isFalling;
    public bool isCrouching;
    public bool isFlipping;
    public bool isIdle;

    [Space]


    bool flipCol = true; //True if facing right, false if facing left

    GameObject cam; //Camera

    Rigidbody2D rb; //Player's rigidbody
    SpriteRenderer playerSprite;

    BoxCollider2D playerCol; //The collider of the player

    //ControlScript control //Standholder for control script

    LayerMask enemyLayer; //To detect enemies

    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        rb = GetComponent<Rigidbody2D>();
        playerCol = GetComponent<BoxCollider2D>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyLayer = LayerMask.GetMask("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
        Jump();
        Crouch();
        Gravity();
        CheckAir();
    }

    void Walk()
    {
        if(control.xMove) //If player moves horizontally
        {
            //Calculates velocity based on speed and direction faced
            rb.velocity = new Vector2(control.xMove*normalSpeed, rb.velocity.y);
        }

        //Play out appropraiate animations
        //If moving, but not crouching or jumping
        if(control.xMove != 0 && !isCrouching && !inAir)
        {
            isWalking = true;
            isIdle = false;
        }

        //If not moving, crouhcing, or jumping
        else if(control.xMove == 0 && !isCrouching && !inAir)
        {
            isWalking = false;
            isIdle = true;
        }
    }

    void Jump()
    {
        if(control.jumpOn) //If player presses up
        {
            if(rb.velocity.y < 0) //If player is falling
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallingMod - 1) * Time.deltaTime;
            }
            else if(rb.velocity.y > 0 && !control.jumpOn) //If the player is in the air and jumps again
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (smallJumpMod - 1) * Time.deltaTime;
            }
        }
    }

    void Crouch()
    {
        if(control.crouching && !control.jumpOn && !inAir)
        {
            rb.velocity *= crouchingMod; //Change to crouching speed
            isCrouching = true;
            isIdle = false;
        }
        else
        {
            isCrouching = false;
        }
    }

    void Gravity()
    {
        if(control.gravity) //If player changes gravity
        {

        }
    }

    void CheckAir()
    {
        if(rb.velocity.y != 0)
        {
            inAir = true;
        }
        else
        {
            inAir = false;
        }
    }
}
