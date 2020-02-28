using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject player;
    private PlayerRespawn respawnScript;

    public static GameManager Instance { get; private set; }

    // If a script will be using the singleton in its awake method, make
    // sure the manager is first to execute with the Script Execution
    // Order project settings
    private void Awake()
    {
        // this depend how you want to handle multiple managers (like when switching/adding scenes)
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        respawnScript = player.GetComponent<PlayerRespawn>();
    }

    public void RespawnPlayer()
    {
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        respawnScript.Respawn();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
