using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlyingDrone : EnemyAI
{
    [SerializeField] float floatStrength = 100f;
    [SerializeField] float velY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundRayDistance = 2.0f;
        groundDetection = transform.Find("Ground Detection");
        detectionScript = transform.Find("Ray Emitter").GetComponent<DetectionScript>();

        if (isFacingLeft)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            speed *= -1;
        }
        velY = transform.position.y;
    }

    void FixedUpdate()
    {
      rb.AddForce(-transform.up*-9.81f); //Keeps the drone flying
    }

    void Movement()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y); //Moevment

        transform.position = new Vector3(transform.position.x, //Creates a bobbing effect
        velY+(Mathf.Sin(Time.fixedTime*Mathf.PI) * floatStrength), transform.position.z);
    }
}
