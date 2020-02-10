using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlyingDrone : EnemyAI
{

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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "GravityManipulator")
        {
            rb.velocity = new Vector2(rb.velocity.x, -9.81f * 2f);
        }
    }
}
