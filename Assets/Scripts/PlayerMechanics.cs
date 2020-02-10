using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerMechanics : MonoBehaviour
{
    [Header("Vertical Movement")]
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float smallJumpMod = 3f; //For double jump
    [SerializeField] float fallingMod = 4f; //Speed of falling
    [SerializeField] int hasJumped = 0;
    [SerializeField] bool gravOn = true;
    [SerializeField] Vector2 jumpDir = Vector2.up;

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
    public bool isFacingLeft = false;

    [Space]

    [Header("Powers")]
    [SerializeField] Dictionary<string, bool> powers = new Dictionary<string, bool>
    {
        {"Multidirection Gravity", false}, {"Ug2", false}, {"Ug3", false}, {"Ug4", false}
    };

    [Header("Grenades")]
    [SerializeField] Dictionary<string, int> grenades = new Dictionary<string, int>
    {
        {"Gravity Manipulator", 0}, {"EMP", 0}, {"Ug3", 0}, {"Ug4", 0}
    };
    [SerializeField] string currentGrenade;

    GameObject cam; //Camera

    Rigidbody2D rb; //Player's rigidbody
    SpriteRenderer playerSprite;

    BoxCollider2D playerCol; //The collider of the player
    PlayerCollisions collisions;
    PlayerControlMapping control; //The control map of the player
    GravityController gravControl; //The mechanics of gravity
    ThrowProjectile throwScript; //Ability to throw items

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
        throwScript = GetComponent<ThrowProjectile>();
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
        Throw();
    }

    void FlipSprite()
    {
        if (control.xMove < 0) //If moving left
        {
            playerSprite.flipX = true;
            isFacingLeft = true;

            playerCol.offset = new Vector2(-playerCol.offset.x, playerCol.offset.y);
        }
        else if (control.xMove > 0) //If moving right
        {
            playerSprite.flipX = false;
            isFacingLeft = false;

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
        if(control.jumpOn && hasJumped < 2) //If player presses up
        {
            //rb.AddForce(jumpDir*jumpSpeed, ForceMode2D.Impulse);
            rb.velocity = jumpDir*jumpSpeed;
            hasJumped += 1;
        }
        if(rb.velocity.y < 0) //If player is falling
        {
            rb.velocity += jumpDir * Physics2D.gravity.y * (fallingMod - 1) * Time.deltaTime;
        }
        else if(rb.velocity.y > 0 && !control.jumpOn) //If the player is in the air and jumps again
        {
            rb.velocity += jumpDir * Physics2D.gravity.y * (smallJumpMod - 1) * Time.deltaTime;
        }
        if(rb.velocity.y == 0)
        {
            hasJumped = 0;
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
        if(control.gravityToggle > 0.5f && gravOn)
        {
            gravOn = !gravOn;
            /*if(LevelManager.current.playerData.gravityPower <= 0) //Prevents gravity toggle when 0;
            {
              return;
            }*/
            gravControl.ChangeGravity(powers["Multidirection Gravity"]); //Changes gravity based on ability
            //LevelManager.current.playerData.gravityPower -= 20; //Each toggle drains gravity bar by 20;
        }
        else if(control.gravityToggle < 0.5f && !gravOn)
        {
          gravOn = !gravOn;
        }

        switch(gravControl.gravDir)
        {
            case GravityController.GravityDirection.Down:
                jumpDir = Vector2.up;
                break;
            case GravityController.GravityDirection.Up:
                jumpDir = Vector2.down;
                break;
            case GravityController.GravityDirection.Right:
                jumpDir = Vector2.left;
                break;
            case GravityController.GravityDirection.Left:
                jumpDir = Vector2.right;
                break;
        }
    }

    void Throw()
    {
        /*if(Input.GetKeyDown(KeyCode.Q))
        {
            if(grenades[currentGrenade] != 0)
            {
                throwScript.ThrowGrenade(currentGrenade, isFacingLeft);
            }
        }*/
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
          LevelManager.current.playerData.grenades = grenades;
          LevelManager.current.playerData.currentGrenade = currentGrenade;

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
          grenades = LevelManager.current.playerData.grenades;
          currentGrenade = LevelManager.current.playerData.currentGrenade;

          transform.position = new Vector3(t_x, t_y, t_z);
        }
    }


}
