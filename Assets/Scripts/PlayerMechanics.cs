using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class PlayerMechanics : MonoBehaviour
{
    [Header("Vertical Movement")]
    [SerializeField] float jumpSpeed = 7f;
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
    [SerializeField] GameObject posRight;
    [SerializeField] GameObject posLeft;
    [SerializeField] string movement;

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
        {"Gravity Manipulator", 5}, {"EMP", 5}, {"Ug3", 5}, {"Ug4", 5}
    };
    [SerializeField] int grenade = 0;
    [SerializeField] string currentGrenade;
    [SerializeField] Image polarImg;
    [SerializeField] Image empImg;
    [SerializeField] bool grenOn = true;

    GameObject cam; //Camera

    Rigidbody2D rb; //Player's rigidbody
    SpriteRenderer playerSprite;

    CapsuleCollider2D playerCol; //The collider of the player
    PlayerCollisions collisions;
    PlayerControlMapping control; //The control map of the player
    GravityController gravControl; //The mechanics of gravity
    ThrowProjectile throwScript; //Ability to throw items
    Fade fade;

    LayerMask enemyLayer; //To detect enemies
    LayerMask wallMask;

    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        rb = GetComponent<Rigidbody2D>();
        playerCol = GetComponent<CapsuleCollider2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        control = GetComponent<PlayerControlMapping>();
        collisions = GetComponent<PlayerCollisions>();
        gravControl = GetComponent<GravityController>();
        throwScript = GetComponent<ThrowProjectile>();
        fade = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Fade>();
        LevelManager.current = new LevelManager();

        posRight = GameObject.Find("GrenadePointRight");
        posLeft = GameObject.Find("GrenadePointLeft");
        wallMask = LayerMask.GetMask("Wall");
        polarImg = GameObject.Find("Polarity Grenade Icon").GetComponent<Image>();
        empImg = GameObject.Find("EMP Grenade Icon").GetComponent<Image>();

        rb.gravityScale = 0; //Sets gravity to zero to let GravityController handle manipulation
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
        if ((movement == "xMove" && control.xMove < 0) || (movement == "vMove" && control.vMove < 0)) //If moving left or up
        {
            isFacingLeft = true;

            playerCol.offset = new Vector2(-playerCol.offset.x, playerCol.offset.y);
        }
        else if ((movement == "xMove" && control.xMove > 0) || (movement == "vMove" && control.vMove > 0)) //If moving right or down
        {
            isFacingLeft = false;

            playerCol.offset = new Vector2(-playerCol.offset.x, playerCol.offset.y);
        }
        if(gravControl.gravDir == GravityController.GravityDirection.Up || gravControl.gravDir == GravityController.GravityDirection.Left)
        {
            playerSprite.flipX = !isFacingLeft;
        }
        else
        {
            playerSprite.flipX = isFacingLeft;
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

        if(movement == "xMove" && control.xMove != 0) //If player moves horizontally
        {
            //Calculates velocity based on speed and direction faced
            rb.velocity = new Vector2(control.xMove*normalSpeed, rb.velocity.y);

        }
        else if(movement == "vMove" && control.vMove != 0) //If player moves vertically
        {
          //Calculates velocity based on speed and direction faced
          rb.velocity = new Vector2(rb.velocity.x, control.vMove*normalSpeed);
        }
        else if(control.xMove == 0 && (gravControl.gravDir == GravityController.GravityDirection.Up || gravControl.gravDir == GravityController.GravityDirection.Down))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else if(control.vMove == 0 && (gravControl.gravDir == GravityController.GravityDirection.Right || gravControl.gravDir == GravityController.GravityDirection.Left))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        //Play out appropraiate animations
        //If moving, but not crouching or jumping
        if(control.xMove != 0 && !isCrouching && !collisions.IsInAir() && movement == "xMove")
        {
            isWalking = true;
            isIdle = false;
            isFalling = false;
            isJumping = false;
        }

        //If not moving, crouhcing, or jumping
        else if(control.xMove == 0 && !isCrouching && !collisions.IsInAir() && movement == "xMove")
        {
            isWalking = false;
            isIdle = true;
            isFalling = false;
            isJumping = false;
        }
        else if(control.vMove != 0 && !isCrouching && !collisions.IsInAir() && movement == "vMove")
        {
            isWalking = true;
            isIdle = false;
            isFalling = false;
            isJumping = false;
        }

        //If not moving, crouhcing, or jumping
        else if(control.vMove == 0 && !isCrouching && !collisions.IsInAir() && movement == "vMove")
        {
            isWalking = false;
            isIdle = true;
            isFalling = false;
            isJumping = false;
        }
        else
        {
           isWalking = false;
           isIdle = false;
        }

    }

    void Jump()
    {
        //When on ground or ceiling, player uses y velocity. For walls, player uses x velocity
        if(gravControl.gravDir == GravityController.GravityDirection.Down || gravControl.gravDir == GravityController.GravityDirection.Up)
        {
            if(control.jumpOn && hasJumped < 2) //If player presses up
            {
                //rb.AddForce(jumpDir*jumpSpeed, ForceMode2D.Impulse);
                rb.velocity = jumpDir*jumpSpeed;
                isJumping = true;
                hasJumped += 1;
            }
            if(rb.velocity.y < 0) //If player is falling
            {
                rb.velocity += jumpDir * Physics2D.gravity.y * (fallingMod - 1) * Time.deltaTime;
                isJumping = false;
                isFalling = true;
            }
            else if(rb.velocity.y > 0 && !control.jumpOn) //If the player is in the air and jumps again
            {
                rb.velocity += jumpDir * Physics2D.gravity.y * (smallJumpMod - 1) * Time.deltaTime;
                isFalling = false;
                isJumping = true;
            }
            if(rb.velocity.y == 0)
            {
                hasJumped = 0;
            }
        }

        if(gravControl.gravDir == GravityController.GravityDirection.Left || gravControl.gravDir == GravityController.GravityDirection.Right)
        {
            if(control.jumpOn && hasJumped < 2) //If player presses up
            {
                //rb.AddForce(jumpDir*jumpSpeed, ForceMode2D.Impulse);
                rb.velocity = jumpDir*jumpSpeed;
                isJumping = true;
                hasJumped += 1;
            }
            if(rb.velocity.x < 0) //If player is falling
            {
                rb.velocity += jumpDir * -9.81f * (fallingMod - 1) * Time.deltaTime;
                isJumping = false;
                isFalling = true;
            }
            else if(rb.velocity.x > 0 && !control.jumpOn) //If the player is in the air and jumps again
            {
                rb.velocity += jumpDir * -9.81f * (smallJumpMod - 1) * Time.deltaTime;
                isFalling = false;
                isJumping = true;
            }
            if(rb.velocity.x == 0)
            {
                hasJumped = 0;
            }
        }
    }

    void Crouch()
    {
        if(control.crouching && !control.jumpOn && !collisions.IsInAir())
        {
            rb.velocity *= crouchingMod; //Change to crouching speed
            isCrouching = true;
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
            if(LevelManager.current.playerData.gravityPower < 20) //Prevents gravity toggle when < 20;
            {
              return;
            }
            gravOn = !gravOn;
            gravControl.ChangeGravity(LevelManager.current.playerData.powers["Multidirection Gravity"]); //Changes gravity based on ability

        }
        else if(control.gravityToggle < 0.5f && !gravOn)
        {
          gravOn = !gravOn;
        }

        switch(gravControl.gravDir)
        {
            case GravityController.GravityDirection.Down:
                jumpDir = Vector2.up;
                movement = "xMove";
                break;
            case GravityController.GravityDirection.Up:
                jumpDir = Vector2.down;
                movement = "xMove";
                break;
            case GravityController.GravityDirection.Right:
                jumpDir = Vector2.left;
                movement = "vMove";
                break;
            case GravityController.GravityDirection.Left:
                jumpDir = Vector2.right;
                movement = "vMove";
                break;
        }
    }

    void Throw()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            grenade += 1;

            if(grenade == 2)
            {
                grenade = 0;
            }
            switch(grenade)
            {
                case 0:
                  StartCoroutine(fade.FadeImageInOut(0.2f, 0.2f, polarImg, 1f));
                  break;
                case 1:
                  StartCoroutine(fade.FadeImageInOut(0.2f, 0.2f, empImg, 1f));
                  break;
                /*case 2:
                  currentGrenade = "GravityManipulator";
                  break;
                case 3:
                  currentGrenade = "GravityManipulator";
                  break;*/
            }

        }
        switch(grenade)
        {
            case 0:
              currentGrenade = "Gravity Manipulator";
              break;
            case 1:
              currentGrenade = "EMP";
              break;
            /*case 2:
              currentGrenade = "GravityManipulator";
              break;
            case 3:
              currentGrenade = "GravityManipulator";
              break;*/
        }


        if(control.throwHeld > 0.5 && grenOn)
        {
            if(grenades[currentGrenade] != 0)
            {
                grenades[currentGrenade] -= 1;
                throwScript.ThrowGrenade(currentGrenade, isFacingLeft);
            }
            grenOn = !grenOn;
        }
        else if(control.throwHeld < 0.5 && !grenOn)
        {
            grenOn = !grenOn;
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
