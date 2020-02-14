using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class GravityController : MonoBehaviour
{
    float fps = 0.5f; //Time for image to appear

    public enum GravityDirection
    {
        Down,
        Up,
        Right,
        Left
    }
    GravityDirection m_gravDir;

    GameObject gravityArrows;
    PlayerControlMapping control;
    PlayerCollisions collisions;
    SpriteRenderer playerSprite;
    Fade fade;
    [SerializeField] Rigidbody2D rb;

    void Start()
    {
        control = GetComponent<PlayerControlMapping>();
        collisions = GetComponent<PlayerCollisions>();
        gravityArrows = GameObject.Find("Gravity Arrows");
        fade = gravityArrows.GetComponent<Fade>();
        m_gravDir = GravityDirection.Down;
        rb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        gravityArrows.SetActive(false);
    }

    void Update()
    {
        if(collisions.IsOnWall() || collisions.IsOnCeiling())
        {
            //LevelManager.current.playerData.gravityPower -= 1f * Time.deltaTime;
        }
        else if(collisions.IsOnGround())
        {
            //LevelManager.current.playerData.gravityPower += 1f * Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        switch(m_gravDir)
        {
            case GravityDirection.Down:
                rb.AddForce(Vector2.down*9.81f*2f);
                if(LevelManager.current.playerData.gravityPower < 100f)
                {
                  LevelManager.current.playerData.gravityPower += 0.1f;
                }
                break;

            case GravityDirection.Up:
                rb.AddForce(Vector2.up*9.81f*2f);
                LevelManager.current.playerData.gravityPower -= 0.1f;
                break;

            case GravityDirection.Left:
                rb.AddForce(-transform.up*9.81f*2f);
                //rb.AddForce(-Vector2.up*-9.81f);
                LevelManager.current.playerData.gravityPower -= 0.1f;
                break;

            case GravityDirection.Right:
                rb.AddForce(-transform.up*9.81f*2f);
                //rb.AddForce(-Vector2.up*-9.81f);
                LevelManager.current.playerData.gravityPower -= 0.1f;
                break;
        }
        if(LevelManager.current.playerData.gravityPower <= 0)
        {
            if(m_gravDir != GravityDirection.Down)
            {
              transform.eulerAngles = new Vector3(0, 0, 0);
            }
            m_gravDir = GravityDirection.Down;
            LevelManager.current.playerData.gravityPower = 0;
        }
    }
    public void ChangeGravity(bool multiDir)
    {
        if(!multiDir)
        {
            LevelManager.current.playerData.gravityPower -= 20; //Each toggle drains gravity bar by 20;
            if(m_gravDir == GravityDirection.Down) //If current direction is down, make up
            {
                m_gravDir = GravityDirection.Up;
            }
            else if(m_gravDir == GravityDirection.Up) //If current direction is up, make down
            {
                m_gravDir = GravityDirection.Down;
            }
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - 180);
            return;
        }

        //MULTIDIRECTIONAL GRAVITY
        gravityArrows.SetActive(true); //Makes the arrows image appear
        StartCoroutine(MultiDirGravity());
        gravityArrows.SetActive(false); //Makes the arrows image disappear
    }

    IEnumerator MultiDirGravity()
    {
      float sTime = 0f;
      while(sTime <= 5f)
      {
          if(Input.GetKey(KeyCode.UpArrow))
          {
              //Physics2D.gravity = new Vector2(0, 9.8f);
              m_gravDir = GravityDirection.Up;
              LevelManager.current.playerData.gravityPower -= 20; //Each toggle drains gravity bar by 20;
              transform.eulerAngles = new Vector3(0, 0, 180);
              break;
          }

          if(Input.GetKey(KeyCode.DownArrow))
          {
            //  Physics2D.gravity = new Vector2(0, -9.8f);
              m_gravDir = GravityDirection.Down;
              LevelManager.current.playerData.gravityPower -= 20; //Each toggle drains gravity bar by 20;
              transform.eulerAngles = new Vector3(0, 0, 0);
              break;
          }

          if(Input.GetKey(KeyCode.RightArrow))
          {
              //Physics2D.gravity = new Vector2(9.8f, 0);
              m_gravDir = GravityDirection.Right;
              LevelManager.current.playerData.gravityPower -= 20; //Each toggle drains gravity bar by 20;
              transform.eulerAngles = new Vector3(0, 0, 90);
              break;
          }

          if(Input.GetKey(KeyCode.LeftArrow))
          {
              //Physics2D.gravity = new Vector2(-9.8f, 0);
              m_gravDir = GravityDirection.Left;
              LevelManager.current.playerData.gravityPower -= 20; //Each toggle drains gravity bar by 20;
              transform.eulerAngles = new Vector3(0, 0, 270);
              break;
          }
          sTime += Time.deltaTime;
          /*if(Time.time - sTime >= 5f)
          {
            break;
          }*/
      }
      yield return null;
    }

    public GravityDirection gravDir
    {
        get{return m_gravDir;}
    }
}
