using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelManager //Is started by Main Menu or when game is started
{
    public static LevelManager current; //Starts a global Level Manager

    public PlayerStats playerData; //Holds the current player data

    public bool isSceneBeingLoaded = false; //Checks if a scene is being loaded

    public GameObject lastCheckpoint; //Saves last checkpoint

    public LevelManager()
    {
        playerData = new PlayerStats();
        playerData.gravityPower = playerData.maxGravityPower;
        playerData.sceneID = 0;
        playerData.playerPosX = 0;
        playerData.playerPosX = 0;
        playerData.playerPosX = 0;
    }

}
