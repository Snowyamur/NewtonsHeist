﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyAI : MonoBehaviour
{
    // ----- Public variables -----
    [Header("Mechanics")]
    [SerializeField] protected float speed = 6.0f;
    [SerializeField] protected bool isFacingLeft = false;  // isFacingLeft is true if the AI is patrolling
                                       // in the left direction
    [SerializeField] protected bool hitEMP = false; //Signals if hit by EMP
    [SerializeField] protected float rayDistance = 2f;
    [SerializeField] protected float gravDelay = 5.0f;
    [SerializeField] protected float timer = 1f;
    [Space]

    [Header("Layer Masks")]
    [SerializeField] protected LayerMask groundMask;
    [SerializeField] protected LayerMask wallMask;
    [SerializeField] protected LayerMask ceilingMask;
    [SerializeField] protected LayerMask enemyMask;
    [SerializeField] protected LayerMask trapMask;
    [Space]

    [Header("Animation Bools")]
    public bool isIdle;
    public bool isWalking;
    public bool isTurning;
    [Space]

    // GameObject component variables
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Transform groundDetection;
    [SerializeField] protected DetectionScript detectionScript;


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

    protected void Patrol()
    {
        CheckGrounded();
        CheckObjects();
        CheckWall();
        Look();
        Movement();
    }

    protected void CheckGrounded()
    {
        RaycastHit2D raycastHit = DrawRaycast(groundDetection.position, -transform.up,
                                              rayDistance, groundMask);

        if (raycastHit.collider == false) //If the enemy hits an object in front of it, it flips direction
        {
            if(timer > 0) //Checks if the enemy spent a second waiting before turning
            {
                isTurning = true; //Stops movement
                timer -= Time.deltaTime;
                return;
            }

            ChangeDirection();
            isTurning = false;
            timer = 1f; //Reset timer
        }
    }

    protected void CheckObjects()
    {
        RaycastHit2D raycastEnemy = DrawRaycast(groundDetection.position, transform.right,
                                              rayDistance, enemyMask);

        RaycastHit2D raycastTrap = DrawRaycast(groundDetection.position, transform.right,
                                              rayDistance, trapMask);

        if ((raycastEnemy.collider == true && raycastEnemy.collider.transform.gameObject != this.gameObject) || raycastTrap.collider == true) //If the enemy hits an object in front of it, it flips direction
        {
            ChangeDirection();
        }
    }

    protected void CheckWall()
    {
        RaycastHit2D raycastHit = DrawRaycast(groundDetection.position, transform.right,
                                              rayDistance, wallMask);

        if (raycastHit.collider == true) //If the enemy hits an object in front of it, it flips direction
        {
            if(timer > 0) //Checks if the enemy spent a second waiting before turning
            {
                isTurning = true; //Stops movement
                timer -= Time.deltaTime;
                return;
            }

            ChangeDirection();
            isTurning = false;
            timer = 1f; //Reset timer
        }
    }

    protected void ChangeDirection()
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

    protected void Look()
    {
        // TODO: improve enemy detection here
        detectionScript.SetAimDirection(Vector2.right, isFacingLeft);
    }

    protected void Movement()
    {
        if(!hitEMP && !isTurning) //If not hit by an EMP
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            isWalking = true;
            isIdle = false;
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            isWalking = false;
            //isIdle = true;
        }
    }

    // ----- HELPER FUNCTIONS -----
    protected RaycastHit2D DrawRaycast(Vector2 origin, Vector2 direction, float distance, LayerMask layerMask)
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(origin, direction, distance, layerMask);
        Debug.DrawRay(origin, direction.normalized * distance, Color.yellow);

        return raycastHit;
    }

    void SwitchGravity()
    {
        StartCoroutine(FlipGravity(gravDelay));

    }
    IEnumerator FlipGravity(float delay)
    {
        rb.velocity = new Vector2(rb.velocity.x, 9.81f * 2f);
        yield return new WaitForSeconds(delay);
        rb.velocity = new Vector2(rb.velocity.x, 0);
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "GravityManipulator")
        {
            SwitchGravity();
        }
        else if(collision.transform.tag == "EMP")
        {
            hitEMP = true; //Signals hit by EMP
        }
        else if(collision.transform.tag == "Obstacle")// || collision.transform.tag == "Enemy")
        {
            ChangeDirection();
        }
    }
}
