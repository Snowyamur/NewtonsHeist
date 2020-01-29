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
        horizontalDpad = Input.GetAxisRaw("AbilityHorizontal");
        verticalDpad = Input.GetAxisRaw("AbilityVertical");
        if(Input.GetButtonDown("Pause"))
        {
            Debug.Log("Start pressed");
        }
        //Debug.Log(horizontalAim + ", " + verticalAim);
    }

    void FixedUpdate()
    {
        Vector2 tempVelocity = Vector2.zero;
        riggie.velocity = new Vector2(horizontalMove * 5, verticalMove * 5);
    }
}
