﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyAI : MonoBehaviour
{
    // ----- Public variables -----
    public float speed = 6.0f;
    public bool isFacingLeft = false;  // isFacingLeft is true if the AI is patrolling
                                       // in the left direction
    public float groundRayDistance = 2.0f;

    // GameObject component variables
    public Rigidbody2D rb;
    public Transform groundDetection;
    public DetectionScript detectionScript;

    public float gravDelay = 5.0f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundDetection = transform.Find("Ground Detection");
        detectionScript = transform.Find("Ray Emitter").GetComponent<DetectionScript>();

        if (isFacingLeft)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            speed *= -1;
        }
    }

    void Update()
    {
        /* ----- code to debug checkpoints -----
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<PlayerRespawn>().Respawn();
        }
        */

        Patrol();
    }

    public void Patrol()
    {
        CheckGrounded();
        Look();
        Movement();
    }

    public void CheckGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(groundDetection.position, -transform.up,
                                                    groundRayDistance);

        if (raycastHit.collider == false) //If the enemy hits an object in front of it, it flips direction
        {
            ChangeDirection();
        }
    }

    public void CheckWall()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(groundDetection.position, transform.right,
                                                    groundRayDistance);

        if (raycastHit.collider == true) //If the enemy hits an object in front of it, it flips direction
        {
            ChangeDirection();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // Patrol changes direction when hitting a wall or
        // an obstacle.
        if (collision.transform.tag == "Obstacle" || collision.transform.tag == "Wall")
        {
            ChangeDirection();
        }
    }

    public void ChangeDirection()
    {
        if (isFacingLeft)
        {
            transform.eulerAngles = new Vector3(0, 0, 0); //Resets angles
        }
        else
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }

        speed *= -1; //Flips speed
        isFacingLeft = !isFacingLeft;
    }

    public void Look()
    {
        // TODO: improve enemy detection here
        if (isFacingLeft)
            detectionScript.SetAimDirection(Vector3.left, true);
        else
            detectionScript.SetAimDirection(Vector3.right, false);
    }

    public void Movement()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    public void SwitchGravity()
    {
        StartCoroutine(FlipGravity(gravDelay));

    }
    public IEnumerator FlipGravity(float delay)
    {
        rb.velocity = new Vector2(rb.velocity.x, 9.81f*2f);
        yield return new WaitForSeconds(delay);
        rb.velocity = new Vector2(rb.velocity.x, 0);
    }
}
