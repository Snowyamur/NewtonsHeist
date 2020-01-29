using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Death : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D obstacle)
    {
        // Attach this script to the Obstacle: Lasers / Electricity / etc
        // This will destroy both the player
        // and the instantiated obstacle (assuming the obstacle is instantiated)
        
        // Obstacle should have the tag "obstacle" (subjected to change)
        if (gameObject.tag == "obstacle")
        {
            Destroy(gameObject);
            // destroys the instantiated obstacle
        }
        // Player should have the tag "Player"
        if (obstacle.tag == "Player")
        {
            Destroy(obstacle.gameObject);
            // destroys the player object
        }
    }
}
