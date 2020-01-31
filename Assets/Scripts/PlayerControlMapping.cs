using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlMapping : MonoBehaviour
{
    Rigidbody2D riggie;

    public float xMove;
    public float verticalMove;
    public float horizontalAim;
    public float verticalAim;
    public bool jumpOn;
    public bool crouching;
    public bool xButton;
    public bool throwHeld;
    public float gravityToggle;
    public float gravityHold;
    public float horizontalDpad;
    public float verticalDpad;
    public bool pause;
    public bool save;
    public bool load;

    bool m_inputting = true; //For debugging only, revert to no value before ppublsihing game

    // JUST A PLACEHOLDER TO REMOVE ERRORS
    // Start is called before the first frame update
    void Awake()
    {
         riggie = GetComponent<Rigidbody2D>();
         xMove = Input.GetAxis("Horizontal");
         verticalMove = Input.GetAxis("Vertical");
         horizontalAim = Input.GetAxis("HorizontalAim");
         verticalAim = Input.GetAxis("VerticalAim");
         jumpOn = Input.GetButtonDown("Jump");
         crouching = Input.GetButtonDown("Crouch");
         xButton = Input.GetButtonDown("Submit");
         throwHeld = Input.GetButtonDown("ThrownHeld");
         gravityToggle = Input.GetAxis("GravityToggle");
         gravityHold = Input.GetAxisRaw("GravityToggle");
         horizontalDpad = Input.GetAxisRaw("AbilityHorizontal");
         verticalDpad = Input.GetAxisRaw("AbilityVertical");
         pause = Input.GetButtonDown("Pause");

         //PLACEHOLDERs
         save = Input.GetKeyDown(KeyCode.F5);
         load = Input.GetKeyDown(KeyCode.F6);
    }

    // Update is called once per frame
    void Update()
    {
        if(m_inputting)
        {
            xMove = Input.GetAxis("Horizontal");
            verticalMove = Input.GetAxis("Vertical");
            horizontalAim = Input.GetAxis("HorizontalAim");
            verticalAim = Input.GetAxis("VerticalAim");
            jumpOn = Input.GetButtonDown("Jump");
            crouching = Input.GetButtonDown("Crouch");
            xButton = Input.GetButtonDown("Submit");
            throwHeld = Input.GetButtonDown("ThrownHeld");
            gravityToggle = Input.GetAxis("GravityToggle");
            gravityHold = Input.GetAxisRaw("GravityToggle");
            horizontalDpad = Input.GetAxisRaw("AbilityHorizontal");
            verticalDpad = Input.GetAxisRaw("AbilityVertical");
            pause = Input.GetButtonDown("Pause");

            //PLACEHOLDERs
            save = Input.GetKeyDown(KeyCode.F5);
            load = Input.GetKeyDown(KeyCode.F6);
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
}
//Can mess with "Snap" and "Gravity" values in Project Settings to
// affect how nice movement feels.
// If we were to do this, turn GetAxisRaw to GetAxis
