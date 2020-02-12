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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundDetection = transform.Find("Ground Detection");
        detectionScript = transform.Find("Ray Emitter").GetComponent<DetectionScript>();
        groundMask = LayerMask.GetMask("Ground");
        wallMask = LayerMask.GetMask("Wall");
        ceilingMask = LayerMask.GetMask("Ceiling");

        rayDistance = 1f;

        if (isFacingLeft)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            speed *= -1;
        }

        gravDir = GravityDirection.Down; //Begin Spider Drone on the ground
    }

    void Update()
    {
        CheckSurface();
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
                rb.AddForce(-transform.right*-9.81f*2f);
                detectionScript.SetAimDirection(new Vector2(0,-1), isFacingLeft);
                break;

            case GravityDirection.Right:
                rb.AddForce(transform.right*-9.81f*2f);
                detectionScript.SetAimDirection(new Vector2(0,1), isFacingLeft);
                break;
        }
    }

    void CheckSurface()
    {
        //Checks for ground, wall, or ceiling to turn onto
        RaycastHit2D rayGround = DrawRaycast(groundDetection.position, transform.right,
                                              rayDistance, groundMask);

        RaycastHit2D rayCeiling = DrawRaycast(groundDetection.position, transform.right,
                                              rayDistance, wallMask);

        RaycastHit2D rayWall = DrawRaycast(groundDetection.position, transform.right,
                                              rayDistance, ceilingMask);

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
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + 90); //Rotate right
            }
            else
            {
                currentGrav -= 1; //Changes direction to the left

                if(currentGrav == -1) //Resets to prevent from underflowing enum
                {
                  currentGrav = 3;
                }
                gravDir = (GravityDirection)currentGrav;
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - 90); //Rotate left
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

            //Debug.Log(gravDir);
        }
    }

    void Movement()
    {
        if(!hitEMP)
        {
            if(gravDir == GravityDirection.Down || gravDir == GravityDirection.Up)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y); //Horizontal Movement
            }

            else if(gravDir == GravityDirection.Left || gravDir == GravityDirection.Right)
            {
              rb.velocity = new Vector2(rb.velocity.x, speed); //Vertical Moevment
            }
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }

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
