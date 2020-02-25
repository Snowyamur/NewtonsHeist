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

    GameObject[] gravityArrows;
    PlayerControlMapping control;
    PlayerCollisions collisions;
    SpriteRenderer playerSprite;
    Fade fade;
    AudioSource audio;
    [SerializeField] Rigidbody2D rb;

    void Start()
    {
        control = GetComponent<PlayerControlMapping>();
        collisions = GetComponent<PlayerCollisions>();
        audio = GetComponent<AudioSource>();
        gravityArrows = new GameObject[4];
        gravityArrows[0] = GameObject.Find("GravUp");
        gravityArrows[1] = GameObject.Find("GravRight");
        gravityArrows[2] = GameObject.Find("GravDown");
        gravityArrows[3] = GameObject.Find("GravLeft");
        m_gravDir = GravityDirection.Down;
        rb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        //gravityArrows.SetActive(false);

        for(int i = 0; i < 4; i++)
        {
            gravityArrows[i].SetActive(false);
        }
    }

    void Update()
    {
        if(LevelManager.current.playerData.powers["Multidirection Gravity"])
        {
            if(Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow)
            && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                gravityArrows[2].SetActive(true);
                gravityArrows[0].SetActive(false);
                gravityArrows[1].SetActive(false);
                gravityArrows[3].SetActive(false);
            }
            else if(!Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow)
            && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                gravityArrows[3].SetActive(true);
                gravityArrows[0].SetActive(false);
                gravityArrows[1].SetActive(false);
                gravityArrows[2].SetActive(false);
            }
            else if(!Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow)
            && Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                gravityArrows[0].SetActive(true);
                gravityArrows[2].SetActive(false);
                gravityArrows[1].SetActive(false);
                gravityArrows[3].SetActive(false);
            }
            else if(!Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow)
            && !Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
            {
                gravityArrows[1].SetActive(true);
                gravityArrows[0].SetActive(false);
                gravityArrows[2].SetActive(false);
                gravityArrows[3].SetActive(false);
            }
            else
            {
              gravityArrows[0].SetActive(false);
              gravityArrows[1].SetActive(false);
              gravityArrows[2].SetActive(false);
              gravityArrows[3].SetActive(false);
            }
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
        audio.Play();
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
        StartCoroutine(MultiDirGravity());

    }

    IEnumerator MultiDirGravity()
    {
      float sTime = 0f;

      while(sTime <= 5f)
      {
          //gravityArrows.SetActive(true); //Makes the arrows image appear
          if(Input.GetKey(KeyCode.UpArrow))
          {
              m_gravDir = GravityDirection.Up;
              LevelManager.current.playerData.gravityPower -= 20; //Each toggle drains gravity bar by 20;
              transform.eulerAngles = new Vector3(0, 0, 180);
              break;
          }

          if(Input.GetKey(KeyCode.DownArrow))
          {
              m_gravDir = GravityDirection.Down;
              LevelManager.current.playerData.gravityPower -= 20; //Each toggle drains gravity bar by 20;
              transform.eulerAngles = new Vector3(0, 0, 0);
              break;
          }

          if(Input.GetKey(KeyCode.RightArrow))
          {
              m_gravDir = GravityDirection.Right;
              LevelManager.current.playerData.gravityPower -= 20; //Each toggle drains gravity bar by 20;
              transform.eulerAngles = new Vector3(0, 0, 90);
              break;
          }

          if(Input.GetKey(KeyCode.LeftArrow))
          {
              m_gravDir = GravityDirection.Left;
              LevelManager.current.playerData.gravityPower -= 20; //Each toggle drains gravity bar by 20;
              transform.eulerAngles = new Vector3(0, 0, 270);
              break;
          }
          sTime += Time.deltaTime;

      }
      //gravityArrows.SetActive(false); //Makes the arrows image disappear
      yield return null;
    }

    public GravityDirection gravDir
    {
        get{return m_gravDir;}
        set{m_gravDir = value;}
    }
}
