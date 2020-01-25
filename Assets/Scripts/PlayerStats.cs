using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerStats
{
    float m_gravityPower; //HOw much gravity the player has currently
    float m_maxGravityPower = 100f; //Max gravity the player has

    //Current position of player in scene
    float m_playerPosX;
    float m_playerPosY;
    float m_playerPosZ;

    int m_sceneID; //Current scene

    Dictionary<string, bool> m_powers = new Dictionary<string, bool>
    {
        {"Multidirection Gravity", false}, {"Ug2", false}, {"Ug3", false}, {"Ug4", false}
    };

    public float gravityPower
    {
      get{return m_gravityPower;}
      set{m_gravityPower = value;}
    }
    public float maxGravityPower
    {
      get{return m_maxGravityPower;}
      set{m_maxGravityPower = value;}
    }
    public float playerPosX
    {
      get{return m_playerPosX;}
      set{m_playerPosX = value;}
    }
    public float playerPosY
    {
      get{return m_playerPosY;}
      set{m_playerPosY = value;}
    }
    public float playerPosZ
    {
      get{return m_playerPosZ;}
      set{m_playerPosZ = value;}
    }
    public int sceneID
    {
      get{return m_sceneID;}
      set{m_sceneID = value;}
    }
    public Dictionary<string, bool> powers
    {
      get{return m_powers;}
      set{m_powers = value;}
    }
}
