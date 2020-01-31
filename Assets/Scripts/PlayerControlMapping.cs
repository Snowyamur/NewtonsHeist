using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlMapping : MonoBehaviour
{
    Rigidbody2D riggie;

    [SerializeField] float m_xMove;
    [SerializeField] float m_vMove;
    [SerializeField] float m_horizontalAim;
    [SerializeField] float m_verticalAim;
    [SerializeField] bool m_jumpOn;
    [SerializeField] bool m_crouching;
    [SerializeField] bool m_enter;
    [SerializeField] float m_throwHeld;
    [SerializeField] float m_gravityToggle;
    [SerializeField] float m_gravityHold;
    [SerializeField] float m_horizontalDpad;
    [SerializeField] float m_verticalDpad;
    [SerializeField] bool m_pause;
    [SerializeField] bool m_save;
    [SerializeField] bool m_load;

    [SerializeField] bool m_inputting = true; //For debugging only, revert to no value before ppublsihing game

    // JUST A PLACEHOLDER TO REMOVE ERRORS
    // Start is called before the first frame update
    void Awake()
    {
         riggie = GetComponent<Rigidbody2D>();
         m_xMove = Input.GetAxis("Horizontal");
         m_vMove = Input.GetAxis("Vertical");
         m_horizontalAim = Input.GetAxis("HorizontalAim");
         m_verticalAim = Input.GetAxis("VerticalAim");
         m_jumpOn = Input.GetButtonDown("Jump");
         m_crouching = Input.GetButton("Crouch");
         m_enter = Input.GetButtonDown("Submit");
         m_throwHeld = Input.GetAxis("ThrownHeld");
         m_gravityToggle = Input.GetAxis("GravityToggle");
         m_gravityHold = Input.GetAxisRaw("GravityToggle");
         m_horizontalDpad = Input.GetAxisRaw("AbilityHorizontal");
         m_verticalDpad = Input.GetAxisRaw("AbilityVertical");
         m_pause = Input.GetButtonDown("Pause");

         //PLACEHOLDERs
         m_save = Input.GetKeyDown(KeyCode.F5);
         m_load = Input.GetKeyDown(KeyCode.F6);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_inputting)
        {
          riggie = GetComponent<Rigidbody2D>();
          m_xMove = Input.GetAxis("Horizontal");
          m_vMove = Input.GetAxis("Vertical");
          m_horizontalAim = Input.GetAxis("HorizontalAim");
          m_verticalAim = Input.GetAxis("VerticalAim");
          m_jumpOn = Input.GetButtonDown("Jump");
          m_crouching = Input.GetButton("Crouch");
          m_enter = Input.GetButtonDown("Submit");
          m_throwHeld = Input.GetAxis("ThrownHeld");
          m_gravityToggle = Input.GetAxis("GravityToggle");
          m_gravityHold = Input.GetAxisRaw("GravityToggle");
          m_horizontalDpad = Input.GetAxisRaw("AbilityHorizontal");
          m_verticalDpad = Input.GetAxisRaw("AbilityVertical");
          m_pause = Input.GetButtonDown("Pause");

          //PLACEHOLDERs
          m_save = Input.GetKeyDown(KeyCode.F5);
          m_load = Input.GetKeyDown(KeyCode.F6);
        }
    }

    public void NoInput()
    {
        m_inputting = false; //Shuts off input for player
    }
    public void StartInput()
    {
        m_inputting = true; //Turns on input for player
    }

    public IEnumerator ToggleInput(float delay) //Toggles input for delay amount
    {
        NoInput();
        yield return new WaitForSeconds(delay);
        StartInput();
    }

    //Public accessors
    public float xMove
    {
        get{return m_xMove;}
    }
    public float vMove
    {
        get{return m_vMove;}
    }
    public float horizontalAim
    {
        get{return m_horizontalAim;}
    }
    public float verticalAim
    {
        get{return m_verticalAim;}
    }
    public bool jumpOn
    {
        get{return m_jumpOn;}
    }
    public bool crouching
    {
        get{return m_crouching;}
    }
    public bool enter
    {
        get{return m_enter;}
    }
    public float throwHeld
    {
        get{return m_throwHeld;}
    }
    public float gravityToggle
    {
        get{return m_gravityToggle;}
    }
    public float gravityHold
    {
        get{return m_gravityHold;}
    }
    public float horizontalDpad
    {
        get{return m_horizontalDpad;}
    }
    public float verticalDpad
    {
        get{return m_verticalDpad;}
    }
    public bool pause
    {
        get{return m_pause;}
    }
    public bool save
    {
        get{return m_save;}
    }
    public bool load
    {
        get{return m_load;}
    }

}
//Can mess with "Snap" and "Gravity" values in Project Settings to
// affect how nice movement feels.
// If we were to do this, turn GetAxisRaw to GetAxis
