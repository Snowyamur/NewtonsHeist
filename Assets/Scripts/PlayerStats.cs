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

    Dictionary<string, int> m_grenades = new Dictionary<string, int>
    {
        {"Gravity Manipulator", 5}, {"EMP", 5}, {"Ug3", 5}, {"Ug4", 5}
    };
    string m_currentGrenade;

    // Collectibles dictionary with {collectible_id (String) : collected (Bool)}
    private Dictionary<string, bool> m_collectibles = new Dictionary<string, bool>();

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
    public Dictionary<string, int> grenades
    {
      get{return m_grenades;}
      set{m_grenades = value;}
    }
    public string currentGrenade
    {
      get{return m_currentGrenade;}
      set{m_currentGrenade = value;}
    }

    public bool collectibleRegistered(string id)
    {
        return m_collectibles.ContainsKey(id);
    }

    public void addCollectible(string id, bool value=false)
    {
        m_collectibles.Add(id, value);
    }

    public void updateCollectibles(string id, bool new_value)
    {
        m_collectibles[id] = new_value;
    }

    public bool getCollectibleItem(string id)
    {
        return m_collectibles[id];
    }
}
