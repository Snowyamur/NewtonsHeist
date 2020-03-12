using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlyingDrone : EnemyAI
{

    void FixedUpdate()
    {
      //Mathf.Sin(Time.fixedTime)
      rb.AddForce(Vector3.down*(-9.81f)); //Keeps the drone flying by counteracting gravity. Adds a bobbing motion
      //Debug.Log((Mathf.Sin(Time.fixedTime)).ToString());
    }

    void Update()
    {
      CheckWall();
      CheckObjects();
      Look();
      Movement();
    }

    void Movement()
    {
        if(!hitEMP && !isTurning) //If not hit by an EMP
        {
            if(transform.eulerAngles.z != 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, speed);
            }
            else
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
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

    void CheckObjects()
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

    void Look()
    {
        detectionScript.SetAimDirection(transform.right, isFacingLeft);
    }

    void ChangeDirection()
    {
        if(transform.eulerAngles.z != 0)
        {
            if(isFacingLeft)
            {
                transform.eulerAngles = new Vector3(0, 0, 90); //Resets angles
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, -90);
            }
        }

        else
        {
            if (isFacingLeft)
            {
                transform.eulerAngles = new Vector3(0, 0, 0); //Resets angles
            }
            else
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
        }


        speed *= -1; //Flips speed
        isFacingLeft = !isFacingLeft;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "GravityManipulator")
        {
            rb.velocity = new Vector2(rb.velocity.x, -9.81f * 2f);
        }
        else if(collision.transform.tag == "EMP")
        {
            hitEMP = true; //Signals hit by EMP
        }
        else if(collision.transform.tag == "Obstacle" || collision.transform.tag == "Enemy")
        {
            ChangeDirection();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Tere");
        if(col.transform.tag == "Trap")
        {
            StartCoroutine(Timer());
        }
    }

    IEnumerator Timer()
    {
        isTurning = true; //Stops movement

        yield return new WaitForSeconds(1f);

        ChangeDirection();
        isTurning = false;
    }
}
