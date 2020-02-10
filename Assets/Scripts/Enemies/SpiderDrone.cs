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

        if (isFacingLeft)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            speed *= -1;
        }

        gravDir = GravityDirection.Down;
    }

    void Update()
    {
        CheckSurface();
        Movement();
    }

    void FixedUpdate() //Responsible for changing gravity of spider drone
    {
        Debug.Log(gravDir);
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

            if(gravDir == GravityDirection.Up || gravDir == GravityDirection.Down) //Flip speed to count for opposite directions
            {
                speed = -speed;
            }
            //Debug.Log(gravDir);
        }
    }

    void Movement()
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
}
