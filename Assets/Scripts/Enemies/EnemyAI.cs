using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyAI : MonoBehaviour
{
    // ----- Public variables -----
    [SerializeField] protected float speed = 6.0f;
    [SerializeField] protected bool isFacingLeft = false;  // isFacingLeft is true if the AI is patrolling
                                       // in the left direction
    [SerializeField] protected float rayDistance = 2.0f;
    [SerializeField] protected LayerMask groundMask;
    [SerializeField] protected LayerMask wallMask;

    // GameObject component variables
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Transform groundDetection;
    [SerializeField] protected DetectionScript detectionScript;

    [SerializeField] protected float gravDelay = 5.0f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundDetection = transform.Find("Ground Detection");
        detectionScript = transform.Find("Ray Emitter").GetComponent<DetectionScript>();
        groundMask = LayerMask.GetMask("Ground");
        wallMask = LayerMask.GetMask("Wall");

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
        CheckWall();
        Look();
        Movement();
    }

    void CheckGrounded()
    {
        RaycastHit2D raycastHit = DrawRaycast(groundDetection.position, -transform.up,
                                              rayDistance, groundMask);

        if (raycastHit.collider == false) //If the enemy hits an object in front of it, it flips direction
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
            ChangeDirection();
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
        if (isFacingLeft)
            detectionScript.SetAimDirection(Vector3.left, true);
        else
            detectionScript.SetAimDirection(Vector3.right, false);
    }

    protected void Movement()
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
    }
}
