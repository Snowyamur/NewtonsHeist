using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyAI : MonoBehaviour
{
    // ----- Public variables -----
    public float speed = 6.0f;
    public bool isFacingLeft = false;  // isFacingLeft is true if the AI is patrolling
                                       // in the left direction
    public float rayDistance = 2.0f;
    [SerializeField] private LayerMask changeDirectionMask;

    // GameObject component variables
    public Rigidbody2D rb;
    public Transform groundDetection;
    public DetectionScript detectionScript;


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
        CheckWall();
        Look();
        Movement();
    }

    public void CheckGrounded()
    {
        RaycastHit2D raycastHit = DrawRaycast(groundDetection.position, -transform.up,
                                              rayDistance, changeDirectionMask);

        if (raycastHit.collider == false) //If the enemy hits an object in front of it, it flips direction
        {
            ChangeDirection();
        }
    }

    public void CheckWall()
    {
        RaycastHit2D raycastHit = DrawRaycast(groundDetection.position, transform.right,
                                              rayDistance, changeDirectionMask);

        if (raycastHit.collider == true) //If the enemy hits an object in front of it, it flips direction
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

    // ----- HELPER FUNCTIONS -----
    private RaycastHit2D DrawRaycast(Vector2 origin, Vector2 direction, float distance, LayerMask layerMask)
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(origin, direction, distance, layerMask);
        Debug.DrawRay(origin, direction.normalized * distance, Color.yellow);

        return raycastHit;
    }
}
