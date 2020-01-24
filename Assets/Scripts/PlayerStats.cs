using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerStats
{
    float m_gravityPower = 100;

    //Current position of player in scene
    float m_playerPosX = 0;
    float m_playerPosY = 0;
    float m_playerPosZ = 0;

    int m_sceneID = 1; //Current scene

    Dictionary<string, bool> m_powers = new Dictionary<string, bool>
    {
        {"Multidirection Gravity", false}, {"Ug2", false}, {"Ug3", false}, {"Ug4", false}
    };

    public float gravityPower
    {
      get{return m_gravityPower;}
      set{m_gravityPower = value;}
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
