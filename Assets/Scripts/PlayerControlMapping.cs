using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlMapping : MonoBehaviour
{
    Rigidbody2D riggie;

    float horizontalMove;
    float verticalMove;
    float horizontalAim;
    float verticalAim;
    bool aButton;
    bool xButton;
    float rTrigger;
    float lTrigger;
    float horizontalDpad;
    float verticalDpad;

    // JUST A PLACEHOLDER TO REMOVE ERRORS
    // Start is called before the first frame update
    void Start()
    {
         riggie = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
        horizontalAim = Input.GetAxis("HorizontalAim");
        verticalAim = Input.GetAxis("VerticalAim");
        aButton = Input.GetButtonDown("Jump");
        xButton = Input.GetButtonDown("Submit");
        rTrigger = Input.GetAxis("ThrownHeld");
        lTrigger = Input.GetAxis("GravityToggle");
        horizontalDpad = Input.GetAxisRaw("AbilityHorizontal");
        verticalDpad = Input.GetAxisRaw("AbilityVertical");
    }
}
