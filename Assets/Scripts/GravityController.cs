using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    void Awake()
    {
        control = GetComponent<PlayerControlMapping>();
        collisions = GetComponent<PlayerCollisions>();
        gravityArrows = GameObject.FindGameObjectWithTag("GravityArrows").GetComponent<Image>();
        fade = gravityArrows.GetComponent<Fade>();
    }

    void Update()
    {
        if(collisions.onWall || collisions.onCeiling)
        {
            LevelManager.current.playerData.gravityPower -= 1f * Time.deltaTime;
        }
        else if(collisions.onGround)
        {
            LevelManager.current.playerData.gravityPower += 1f * Time.deltaTime;
        }
    }

    public void ChangeGravity(bool multiDir)
    {
        if(!multiDir)
        {
            if(m_gravDir == GravityDirection.Down) //If current direction is down, make up
            {
                Physics2D.gravity = new Vector3(0, 9.81f, 0);
            }
            else if(m_gravDir == GravityDirection.Up) //If current direction is up, make down
            {
                Physics2D.gravity = new Vector3(0, -9.81f, 0);
            }
            return;
        }
        //MULTIDIRECTIONAL GRAVITY
        StartCoroutine(fade.FadeImageToFullAlpha(fps, gravityArrows)); //Makes the arrows image appear
        while(control.gravity)
        {
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                Physics2D.gravity = new Vector2(0, 9.8f);
                break;
            }

            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                Physics2D.gravity = new Vector2(0, -9.8f);
                break;
            }

            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                Physics2D.gravity = new Vector2(9.8f, 0);
                break;
            }

            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Physics2D.gravity = new Vector2(-9.8f, 0);
                break;
            }
        }
        StartCoroutine(fade.FadeImageToZeroAlpha(fps, gravityArrows)); //Makes the arrows image disappear
    }
}
