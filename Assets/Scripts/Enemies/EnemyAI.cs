using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // ----- Public variables -----
    [Header("Movement")]
    [SerializeField] float speed = 8.0f;
    [SerializeField] bool isFacingLeft = false;  // isFacingLeft is true if the AI is patrolling
                                                 // in the left direction

    [Header("AI")]
    [SerializeField] float groundRayDistance = 2.0f;

    // ----- Private variables -----
    // GameObject component variables
    private Rigidbody2D rb;
    private Transform groundDetection;
    private DetectionScript detectionScript;


    private void Start()
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

    private void Update()
    {
        /* ----- code to debug checkpoints -----
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<PlayerRespawn>().Respawn();
        }
        */

        Patrol();
    }

    private void Patrol()
    {
        CheckGrounded();
        Look();
        Movement();
    }

    private void CheckGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(groundDetection.position, Vector2.down, 
                                                    groundRayDistance);

        if (raycastHit.collider == false)
        {
            ChangeDirection();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Patrol changes direction when hitting a wall or 
        // an obstacle.
        if (collision.transform.tag == "Obstacle")
        {
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        if (isFacingLeft)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else
            transform.eulerAngles = new Vector3(0, -180, 0);

        speed *= -1;
        isFacingLeft = !isFacingLeft;
    }

    private void Look()
    {
        // TODO: improve enemy detection here
        if (isFacingLeft)
            detectionScript.SetAimDirection(Vector3.left, true);
        else
            detectionScript.SetAimDirection(Vector3.right, false);
    }

    private void Movement()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }
}
