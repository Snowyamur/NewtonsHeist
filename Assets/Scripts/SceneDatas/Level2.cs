using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Holds a scene's data
[System.Serializable]
public class Level2 : SceneData
{
    //Scene name to transfer to
    string scene = "Level 2";
    int nextScene = 3;

    //Player's beginning position in new scene
    float m_xPos = 76f;
    float m_yPos = 0.5f;

    //Scene's boundaries
    float m_maxX = 353.5f;
    float m_minX = 87f;
    float m_maxY = 5f;
    float m_minY = -70.5f;

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
