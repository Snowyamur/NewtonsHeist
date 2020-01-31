using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class GravityController : MonoBehaviour
{
    float fps = 0.5f; //Time for image to appear

    enum GravityDirection
    {
        Down,
        Up,
        Right,
        Left
    }
    GravityDirection m_gravDir;

    Image gravityArrows;
    PlayerControlMapping control;
    PlayerCollisions collisions;
    Fade fade;
    [SerializeField] Rigidbody2D rb;

    void Start()
    {
        control = GetComponent<PlayerControlMapping>();
        collisions = GetComponent<PlayerCollisions>();
        gravityArrows = GameObject.FindGameObjectWithTag("GravityArrows").GetComponent<Image>();
        fade = gravityArrows.GetComponent<Fade>();
        m_gravDir = GravityDirection.Down;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(collisions.IsOnWall() || collisions.IsOnCeiling())
        {
            LevelManager.current.playerData.gravityPower -= 1f * Time.deltaTime;
        }
        else if(collisions.IsOnGround())
        {
            LevelManager.current.playerData.gravityPower += 1f * Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        switch(m_gravDir)
        {
            case GravityDirection.Down:
                rb.AddForce(transform.up*-9.81f*2f);
                break;

            case GravityDirection.Up:
                rb.AddForce(-transform.up*-9.81f*2f);
                break;

            case GravityDirection.Left:
                rb.AddForce(-transform.right*-9.81f*2f);
                break;

            case GravityDirection.Right:
                rb.AddForce(transform.right*-9.81f*2f);
                break;
        }
    }
    public void ChangeGravity(bool multiDir)
    {
        if(!multiDir)
        {
            if(m_gravDir == GravityDirection.Down) //If current direction is down, make up
            {
                m_gravDir = GravityDirection.Up;
            }
            else if(m_gravDir == GravityDirection.Up) //If current direction is up, make down
            {
                m_gravDir = GravityDirection.Down;
            }
            return;
        }

        //MULTIDIRECTIONAL GRAVITY
        StartCoroutine(fade.FadeImageToFullAlpha(fps, gravityArrows)); //Makes the arrows image appear
        while(control.gravityHold != 0)
        {
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                Physics2D.gravity = new Vector2(0, 9.8f);
                m_gravDir = GravityDirection.Up;
                break;
            }

            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                Physics2D.gravity = new Vector2(0, -9.8f);
                m_gravDir = GravityDirection.Down;
                break;
            }

            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                Physics2D.gravity = new Vector2(9.8f, 0);
                m_gravDir = GravityDirection.Right;
                break;
            }

            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Physics2D.gravity = new Vector2(-9.8f, 0);
                m_gravDir = GravityDirection.Left;
                break;
            }
        }
        StartCoroutine(fade.FadeImageToZeroAlpha(fps, gravityArrows)); //Makes the arrows image disappear
    }
}
