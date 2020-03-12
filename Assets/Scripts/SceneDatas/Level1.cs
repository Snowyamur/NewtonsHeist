using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Holds a scene's data
[System.Serializable]
public class Level1 : SceneData
{
    //Scene name to transfer to
    string scene = "Level 1";
    int nextScene = 2;

    //Player's beginning position in new scene
    float m_xPos = -14f;
    float m_yPos = 15f;

    //Scene's boundaries
    float m_maxX = 55f;
    float m_minX = -8.5f;
    float m_maxY = 9.5f;
    float m_minY = -53f;

    //Getters add setters
    public override string getScene() {return scene;}

    public override int getNextScene() {return nextScene;}

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
