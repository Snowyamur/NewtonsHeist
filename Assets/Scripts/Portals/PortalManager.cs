using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    // ----- Private Variables ----- 
    private GameObject player;
    private Rigidbody2D playerRB;

    private GameObject portal1;
    private GameObject portal2;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody2D>();

        portal1 = transform.GetChild(0).gameObject;
        portal2 = transform.GetChild(1).gameObject;
    }

    public void TeleportPlayer(GameObject enterPortal)
    {
        GameObject exitPortal;
        if (enterPortal == portal1)
            exitPortal = portal2;
        else
            exitPortal = portal1;

        // Retain player velocity after exiting the portal
        Vector3 exitPortalNormal = exitPortal.GetComponent<PortalScript>().GetPortalNormal();
        Vector3 exitVelocity = exitPortalNormal * playerRB.velocity.magnitude;
        playerRB.velocity = exitVelocity;

        //set the player position to just in front of the portal.
        player.transform.position = exitPortal.transform.position + exitPortalNormal * 3;
    }
}
