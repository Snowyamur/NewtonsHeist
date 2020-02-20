using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollectibleScript : MonoBehaviour
{
    private PlayerStats ps;
    private string goName;


    private void Start()
    {
        ps = LevelManager.current.playerData;
        goName = gameObject.name;

        // Add collectible to the save stats
        if (!ps.collectibleRegistered(goName)) {
            ps.addCollectible(goName);
        }

        bool collected = ps.getCollectibleItem(goName);
        if (collected)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ps.updateCollectibles(goName, true);
            Destroy(gameObject);
        }
    }
}
