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
        speed = 3f;
        groundDetection = transform.Find("Ground Detection");
        detectionScript = transform.Find("Ray Emitter").GetComponent<DetectionScript>();

        if (isFacingLeft)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            speed *= -1;
        }
        gravDir = GravityDirection.Down;
    }

    void Update()
    {
        Movement();
    }

    void FixedUpdate() //Responsible for changing gravity of spider drone
    {
        switch(gravDir)
        {
            case GravityDirection.Down:
                //rb.AddForce(transform.up*-9.81f*2f);
                break;

            case GravityDirection.Up:
                rb.AddForce(transform.up*-9.81f*2f);
                break;

            case GravityDirection.Left:
                rb.AddForce(-transform.right*-9.81f*2f);
                break;

            case GravityDirection.Right:
                rb.AddForce(transform.right*-9.81f*2f);
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Obstacle") //Will flip direction upon hitting an obstacle
        {
            ChangeDirection();
        }
        else if(collision.transform.tag == "Wall" || collision.transform.tag == "Ceiling" || collision.transform.tag == "Ground") //If the drone hits a surface
        {
            int currentGrav = (int)gravDir; //To cycle through enum

            //Debug.Log(collision.transform.tag);
            if(!isFacingLeft)
            {
                currentGrav += 1;

                if(currentGrav == 4) //Prevent from overloading enum
                {
                  currentGrav = 0;
                }
                gravDir = (GravityDirection)currentGrav;
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + 90); //Rotate right
            }
            else
            {
                currentGrav -= 1;

                if(currentGrav == -1) //Prevent from underflowing enum
                {
                  currentGrav = 3;
                }
                gravDir = (GravityDirection)currentGrav;
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - 90); //Rotate left
            }

            if(gravDir == GravityDirection.Up || gravDir == GravityDirection.Left) //Flip speed to count for opposite directions
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
