using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Holds a scene's data
[System.Serializable]
public class Level3 : SceneData
{
    //Scene name to transfer to
    string scene = "Level 3";

    //Player's beginning position in new scene
    float m_xPos = 7f;
    float m_yPos = 66.5f;

    //Scene's boundaries
    float m_maxX = 160f;
    float m_minX = 1.2f;
    float m_maxY = 68f;
    float m_minY = -18.5f;

    public string getScene() {return scene;}

    public override float xPos
    {
      get{return m_xPos;}
    }
    public override float yPos
    {
      get{return m_yPos;}
    }
    public override float maxX
    {
      get{return m_maxX;}
    }
    public override float minX
    {
      get{return m_minX;}
    }
    public override float maxY
    {
      get{return m_maxY;}
    }
    public override float minY
    {
      get{return m_minY;}
    }

}
