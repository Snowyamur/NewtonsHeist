using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerMechanics : MonoBehaviour
{
    [Header("Vertical Movement")]
    [SerializeField] float JumpSpeed = 200f;
    [SerializeField] float smallJumpMod = 2f; //For double jump
    [SerializeField] float fallingMod = 2.5f; //Speed of falling

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

    [SerializeField] Dictionary<string, bool> powers = new Dictionary<string, bool>
    {
        {"Multidirection Gravity", false}, {"Ug2", false}, {"Ug3", false}, {"Ug4", false}
    };

    GameObject cam; //Camera

    Rigidbody2D rb; //Player's rigidbody
    SpriteRenderer playerSprite;

    BoxCollider2D playerCol; //The collider of the player
    PlayerCollisions collisions;
    PlayerControlMapping control; //The control map of the player
    GravityController gravControl; //The mechanics of gravity

    LayerMask enemyLayer; //To detect enemies

    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        rb = GetComponent<Rigidbody2D>();
        playerCol = GetComponent<BoxCollider2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        control = GetComponent<PlayerControlMapping>();
        collisions = GetComponent<PlayerCollisions>();
        gravControl = GetComponent<GravityController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyLayer = LayerMask.GetMask("Enemy");
    }


    // Update is called once per frame
    void Update()
    {
        FlipSprite();
        Walk();
        Jump();
        Crouch();
        Gravity();
    }

    void FlipSprite()
    {
        if (control.xMove < 0) //If moving left
        {
            playerSprite.flipX = true;

            playerCol.offset = new Vector2(-playerCol.offset.x, playerCol.offset.y);
        }
        else if (control.xMove > 0) //If moving right
        {
            playerSprite.flipX = false;

            playerCol.offset = new Vector2(-playerCol.offset.x, playerCol.offset.y);
        }
        /*if (gravDir = "Up") //If on ceiling
        {
            if(!flipCol)
            {
                pCol.offset = new Vector2(pCol.offset.x, -pCol.offset.y);
            }
        }
        else if (gravDir = "Down") //If on ground
        {
            if(!flipCol)
            {
                pCol.offset = new Vector2(pCol.offset.x, -pCol.offset.y);
            }
        }*/
    }

    void Walk()
    {

        if(control.xMove != 0) //If player moves horizontally
        {
            //Calculates velocity based on speed and direction faced
            rb.velocity = new Vector2(control.xMove*normalSpeed, rb.velocity.y);
        }

        //Play out appropraiate animations
        //If moving, but not crouching or jumping
        if(control.xMove != 0 && !isCrouching && !collisions.IsInAir())
        {
            isWalking = true;
            isIdle = false;
        }

        //If not moving, crouhcing, or jumping
        else if(control.xMove == 0 && !isCrouching && !collisions.IsInAir())
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
            else if(rb.velocity.y > 0 && control.jumpOn) //If the player is in the air and jumps again
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (smallJumpMod - 1) * Time.deltaTime;
            }
        }
    }

    void Crouch()
    {
        if(control.crouching && !control.jumpOn && !collisions.IsInAir())
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
        if(control.gravityToggle != 0)
        {
            gravControl.ChangeGravity(powers["Multidirection Gravity"]); //Changes gravity based on ability
        }
    }

    void SavenLoad()
    {
        if(control.save)
        {
          Scene scene = SceneManager.GetActiveScene();
          LevelManager.current.playerData.sceneID = scene.buildIndex;
          LevelManager.current.playerData.playerPosX = transform.position.x;
          LevelManager.current.playerData.playerPosY = transform.position.y;
          LevelManager.current.playerData.playerPosZ = transform.position.z;

          LevelManager.current.playerData.powers = powers;

          SaveLoad.Save();
        }

        //-----------------------------------------------------------------

        if(control.load)
        {
          SaveLoad.Load();
          LevelManager.current.isSceneBeingLoaded = true;
          int whichScene = LevelManager.current.playerData.sceneID;
          SceneManager.LoadScene(whichScene);

          float t_x = LevelManager.current.playerData.playerPosX;
          float t_y = LevelManager.current.playerData.playerPosY;
          float t_z = LevelManager.current.playerData.playerPosZ;

          powers = LevelManager.current.playerData.powers;

          transform.position = new Vector3(t_x, t_y, t_z);
        }
    }


}
