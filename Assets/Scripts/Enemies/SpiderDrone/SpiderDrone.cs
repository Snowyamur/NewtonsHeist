using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpiderDrone : EnemyAI
{
    enum GravityDirection
    {
        Down,
        Right,
        Up,
        Left
    }
    GravityDirection gravDir;

    bool changed = false; //Checks if the enemy switched grounds

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundDetection = transform.Find("Ground Detection");
        detectionScript = transform.Find("Ray Emitter").GetComponent<DetectionScript>();
        groundMask = LayerMask.GetMask("Ground");
        wallMask = LayerMask.GetMask("Wall");
        ceilingMask = LayerMask.GetMask("Ceiling");
        enemyMask = LayerMask.GetMask("Enemy");
        trapMask = LayerMask.GetMask("Trap");

        rayDistance = 1.5f;

        if (isFacingLeft)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            speed *= -1;
        }

        //Set beginning orientation of drone based on what direction it's currently set
        RaycastHit2D rayGround = DrawRaycast(groundDetection.position, -transform.up,
                                              rayDistance, groundMask);

        RaycastHit2D rayCeiling = DrawRaycast(groundDetection.position, -transform.up,
                                              rayDistance, ceilingMask);

        RaycastHit2D rayWall = DrawRaycast(groundDetection.position, -transform.up,
                                              rayDistance, wallMask);

        if(rayGround)
        {
            gravDir = GravityDirection.Down; //Begin Spider Drone on the ground
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(rayCeiling)
        {
            speed *= -1;
            gravDir = GravityDirection.Up; //Begin Spider Drone on the ceiling
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else if(rayWall)
        {
            if(transform.eulerAngles.z == 90)
            {
              gravDir = GravityDirection.Right; //Begin Spider Drone on the wall
              transform.eulerAngles = new Vector3(0, 0, 90);
            }

            else
            {
              speed *= -1;
              gravDir = GravityDirection.Left; //Begin Spider Drone on the wall
              transform.eulerAngles = new Vector3(0, 0, -90);
            }
        }
        /*else
        {
            gravDir = GravityDirection.Down;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }*/


    }

    void Update()
    {
        StartCoroutine(CheckGrounded());
        //CheckObjects();
        //CheckSurface();
        Movement();
    }

    void FixedUpdate() //Responsible for changing gravity of spider drone based on current surface
    {
        //Debug.Log(gravDir);
        switch(gravDir)
        {
            case GravityDirection.Down:
                //rb.AddForce(-transform.up*-9.81f*2f);
                detectionScript.SetAimDirection(new Vector2(1,0), isFacingLeft);
                break;

            case GravityDirection.Up:
                rb.AddForce(transform.up*-9.81f*2f);
                detectionScript.SetAimDirection(new Vector2(-1,0), isFacingLeft);
                break;

            case GravityDirection.Left:
                rb.AddForce(-transform.right*-9.81f*3f);
                detectionScript.SetAimDirection(new Vector2(0,-1), isFacingLeft);
                break;

            case GravityDirection.Right:
                rb.AddForce(transform.right*-9.81f*3f);
                detectionScript.SetAimDirection(new Vector2(0,1), isFacingLeft);
                break;
        }
    }

    IEnumerator CheckGrounded()
    {
        if(changed)
        {
          yield break;
        }

        RaycastHit2D rayGroundGround = DrawRaycast(groundDetection.position, -transform.up,
                                              rayDistance, groundMask);

        RaycastHit2D rayCeilingGround = DrawRaycast(groundDetection.position, -transform.up,
                                              rayDistance, ceilingMask);

        RaycastHit2D rayWallGround = DrawRaycast(groundDetection.position, -transform.up,
                                              rayDistance, wallMask);

        RaycastHit2D rayGroundNext = DrawRaycast(groundDetection.position, transform.right,
                                              rayDistance, groundMask);

        RaycastHit2D rayCeilingNext = DrawRaycast(groundDetection.position, transform.right,
                                              rayDistance, ceilingMask);

        RaycastHit2D rayWallNext = DrawRaycast(groundDetection.position, transform.right,
                                              rayDistance, wallMask);

        if(gravDir == GravityDirection.Down)
        {
            if (!rayGroundGround && rayWallGround.collider == false && rayWallNext.collider == false) //If the enemy is on the ground and the ground ends
            {
                ChangeDirection();
                yield return new WaitForSeconds(0.5f);
                changed = true;
            }
        }
        else if(gravDir == GravityDirection.Up)
        {
            if (!rayGroundGround && !rayCeilingGround && !rayWallGround && !rayWallNext && !rayCeilingNext && !rayGroundNext) //If the enemy is on the ceiling and the ceiling ends
            {
                ChangeDirection();
                yield return new WaitForSeconds(0.5f);
                changed = true;
            }
        }
        else if(gravDir == GravityDirection.Left || gravDir == GravityDirection.Right)
        {
            if (!rayWallGround && !rayCeilingGround && !rayGroundGround && !rayCeilingNext && !rayGroundNext) //If the enemy is on the wall and the wall ends
            {
                ChangeDirection();
                yield return new WaitForSeconds(0.5f);
                changed = true;
            }
        }

        if(!changed)
        {
            changed = true;
            CheckSurface(); //rayGroundNext, rayCeilingNext, rayWallNext
            yield return new WaitForSeconds(0.5f);
        }

        changed = false;
    }

    void CheckSurface() //RaycastHit2D rayGround, RaycastHit2D rayCeiling, RaycastHit2D rayWall
    {
        //Checks for ground, wall, or ceiling to turn onto
        RaycastHit2D rayGround = DrawRaycast(groundDetection.position, transform.right,
                                              rayDistance, groundMask);

        RaycastHit2D rayCeiling = DrawRaycast(groundDetection.position, transform.right,
                                              rayDistance, ceilingMask);

        RaycastHit2D rayWall = DrawRaycast(groundDetection.position, transform.right,
                                              rayDistance, wallMask);

        if(rayGround || rayCeiling || rayWall) //If the drone hits a surface
        {
            int currentGrav = (int)gravDir; //To cycle through enum

            //Debug.Log(collision.transform.tag);
            if(!isFacingLeft)
            {
                currentGrav += 1; //Changes direction to the right

                if(currentGrav == 4) //Resets to prevent from overloading enum
                {
                  currentGrav = 0;
                }
                gravDir = (GravityDirection)currentGrav;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x+180, transform.eulerAngles.y+180, transform.eulerAngles.z + 90); //Rotate right
            }
            else
            {
                currentGrav -= 1; //Changes direction to the left

                if(currentGrav == -1) //Resets to prevent from underflowing enum
                {
                  currentGrav = 3;
                }
                gravDir = (GravityDirection)currentGrav;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x-180, transform.eulerAngles.y-180, transform.eulerAngles.z - 90); //Rotate left
            }

            if(!isFacingLeft)
            {
                if(gravDir == GravityDirection.Up || gravDir == GravityDirection.Down) //Flip speed to count for opposite directions
                {
                    speed = -speed;
                }
            }
            else
            {
                if(gravDir == GravityDirection.Left || gravDir == GravityDirection.Right) //Flip speed to count for opposite directions
                {
                    speed = -speed;
                }
            }
        }
    }

    void Movement()
    {
        if(!hitEMP)
        {
            if(gravDir == GravityDirection.Down || gravDir == GravityDirection.Up)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y); //Horizontal Movement
                isWalking = true;
            }

            else if(gravDir == GravityDirection.Left || gravDir == GravityDirection.Right)
            {
              rb.velocity = new Vector2(rb.velocity.x, speed); //Vertical Moevment
                isWalking = true;
            }
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            isWalking = false;
        }

    }

    void ChangeDirection()
    {
        if(gravDir == GravityDirection.Down || gravDir == GravityDirection.Up)
        {
            if(isFacingLeft)
            {
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y+180, transform.eulerAngles.z); //Resets angles
            }
            else
            {
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y-180, transform.eulerAngles.z);
            }
        }

        else if(gravDir == GravityDirection.Left || gravDir == GravityDirection.Right)
        {
            if(isFacingLeft)
            {
                Debug.Log("Tere");
                transform.eulerAngles = new Vector3(transform.eulerAngles.x+180, 0, transform.eulerAngles.z); //Resets angles
            }
            else
            {
                Debug.Log("Here");
                transform.eulerAngles = new Vector3(transform.eulerAngles.x-180, 0, transform.eulerAngles.z);
            }
        }


        speed *= -1; //Flips speed
        isFacingLeft = !isFacingLeft;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "GravityManipulator")
        {
            int currentGrav = (int)gravDir; //To cycle through enum

            //Debug.Log(collision.transform.tag);
            if(!isFacingLeft)
            {
                currentGrav += 2; //Changes direction to the right

                if(currentGrav >= 4) //Resets to prevent from overloading enum
                {
                  currentGrav -= 4;
                }
                gravDir = (GravityDirection)currentGrav;
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + 180); //Rotate right
            }
            else
            {
                currentGrav -= 2; //Changes direction to the left

                if(currentGrav <= -1) //Resets to prevent from underflowing enum
                {
                  currentGrav += 4;
                }
                gravDir = (GravityDirection)currentGrav;
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - 180); //Rotate left
            }

            speed = -speed;
        }
        else if(collision.transform.tag == "EMP")
        {
            hitEMP = true; //Signals hit by EMP
        }
        else if(collision.transform.tag == "Obstacle" || collision.transform.tag == "Enemy")
        {
            ChangeDirection();
        }
    }
}
