using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlyingDrone : EnemyAI
{

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rayDistance = 2.0f;
        groundDetection = transform.Find("Ground Detection");
        detectionScript = transform.Find("Ray Emitter").GetComponent<DetectionScript>();

        if (isFacingLeft)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            speed *= -1;
        }
    }

    void FixedUpdate()
    {
      //Mathf.Sin(Time.fixedTime)
      rb.AddForce(-transform.up*(-9.81f)); //Keeps the drone flying by counteracting gravity. Adds a bobbing motion
      //Debug.Log((Mathf.Sin(Time.fixedTime)).ToString());
    }

    void Update()
    {
      CheckWall();
      Look();
      Movement();
    }

    void Movement()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y); //Moevment

        //transform.position = new Vector3(transform.position.x, //Creates a bobbing effect
        //velY+(Mathf.Sin(Time.fixedTime*Mathf.PI) * floatStrength), transform.position.z);
    }
}
