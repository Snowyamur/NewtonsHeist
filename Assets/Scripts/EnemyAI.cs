using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // ----- Public variables -----
    [Header("Movement")]
    [SerializeField] float speed = 8.0f;
    [SerializeField] float groundRayDistance = 2.0f;

    [Header("Detection")]
    [Range(0.0f, 90.0f)]
    [SerializeField] float detectionConeAngle;
    [SerializeField] float detectionRayDistance = 5.0f;

    // ----- Private variables -----
    // GameObject component variables
    private Rigidbody2D rb;
    private Transform groundDetection;
    private Transform detectionCone;

    // GameObejct properties variables
    private bool isFacingLeft = false;  // isFacingLeft is true if the AI is patrolling
                                        // in the left direction

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundDetection = transform.Find("Ground Detection");
        detectionCone   = transform.Find("Detection Cone");
    }

    private void Update()
    {
        Detect();
        Patrol();
    }

    private void Detect()
    {
        Vector2 direction;
        if (isFacingLeft)
            direction = Vector2.left;
        else
            direction = Vector2.right;

        RaycastHit2D raycastHit = DrawRaycast(detectionCone.position, direction,
                                              detectionRayDistance);
    }

    private void Patrol()
    {
        CheckGrounded();
        Movement();
    }

    private void CheckGrounded()
    {
        RaycastHit2D raycastHit = DrawRaycast(groundDetection.position, Vector2.down, 
                                              groundRayDistance);

        if (raycastHit.collider == false)
        {
            if (isFacingLeft)
                transform.eulerAngles = new Vector3(0, 0, 0);
            else
                transform.eulerAngles = new Vector3(0, -180, 0);

            speed *= -1;
            isFacingLeft = !isFacingLeft;
        }
    }

    private void Movement()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    // ----- HELPER FUNCTIONS -----
    private RaycastHit2D DrawRaycast(Vector2 origin, Vector2 direction, float distance)
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(origin, direction, distance);
        Debug.DrawLine(origin, origin + direction * distance, Color.green);

        return raycastHit;
    }
}
