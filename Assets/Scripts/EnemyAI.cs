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
    [Range(0.0f, 180.0f)]
    [SerializeField] float detectionConeAngle = 45.0f;
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
        detectionCone   = transform.Find("Ray Emitter");
    }

    private void Update()
    {
        Detect();
        Patrol();
    }

    private void Detect()
    {
        // Create a cone of detection
        for (float i = -detectionConeAngle; i <= detectionConeAngle; ++i)
        {
            Vector2 direction = CalcRayDir(i);
            RaycastHit2D coneRaycast = DrawRaycast(detectionCone.position, direction, 
                                                   detectionRayDistance);

            if (coneRaycast.collider != false && coneRaycast.transform.tag == "Player")
            {
                Debug.Log("Player killed"); 

                // TODO: implement checkpoint respawn
            }
        }
        
    }

    private Vector2 CalcRayDir(float angle)
    {
        // Determine whether to cast rays left or right
        float baseDir;
        if (isFacingLeft)
            baseDir = Vector2.left.x;
        else
            baseDir = Vector2.right.x;

        float radians = DegreesToRad(angle);
        float height  = CalcHeight(baseDir, radians);

        Vector2 angledDir = new Vector2(baseDir, height);

        return angledDir;
    }

    private void Patrol()
    {
        CheckGrounded();
        Movement();
    }

    private void CheckGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(groundDetection.position, Vector2.down, 
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
        Debug.DrawRay(origin, direction.normalized * distance, Color.yellow);

        return raycastHit;
    }

    private float DegreesToRad(float degree)
    {
        return degree * Mathf.PI / 180.0f;
    }

    private float CalcHeight(float baseLen, float radian)
    {
        // FORMULA: tan(angle) = height / base
        return Mathf.Tan(radian) * baseLen;
    }
}
