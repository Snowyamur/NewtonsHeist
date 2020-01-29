using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlMapping : MonoBehaviour
{
    Rigidbody2D riggie;

    float xMove;
    float verticalMove;
    float horizontalAim;
    float verticalAim;
    bool jumpOn;
    bool crouching;
    bool xButton;
    float rTrigger;
    float gravityToggle;
    float horizontalDpad;
    float verticalDpad;
    bool pause;

    // JUST A PLACEHOLDER TO REMOVE ERRORS
    // Start is called before the first frame update
    void Start()
    {
         riggie = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        xMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
        horizontalAim = Input.GetAxis("HorizontalAim");
        verticalAim = Input.GetAxis("VerticalAim");
        jumpOn = Input.GetButtonDown("Jump");
        crouching = Input.GetButtonDown("Crouch");
        xButton = Input.GetButtonDown("Submit");
        rTrigger = Input.GetAxis("ThrownHeld");
        gravityToggle = Input.GetAxis("GravityToggle");
        horizontalDpad = Input.GetAxisRaw("AbilityHorizontal");
        verticalDpad = Input.GetAxisRaw("AbilityVertical");
        pause = Input.GetButtonDown("Pause");
    }
}
//Can mess with "Snap" and "Gravity" values in Project Settings to
// affect how nice movement feels.