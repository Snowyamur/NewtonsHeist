using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Holds a scene's data
public class SceneData : MonoBehaviour
{
    //Scene name to transfer to
    string scene;

    //Player's beginning position in new scene
    float m_xPos;
    float m_yPos;

    float m_maxX;
    float m_minX;
    float m_maxY;
    float m_minY;

    public string getScene() {return scene;}

    public float xPos
    {
      get{return m_xPos;}
      set{m_xPos = value;}
    }
    public float yPos
    {
      get{return m_yPos;}
      set{m_yPos = value;}
    }
    public float maxX
    {
      get{return m_maxX;}
      set{m_maxX = value;}
    }
    public float minX
    {
      get{return m_minX;}
      set{m_minX = value;}
    }
    public float maxY
    {
      get{return m_maxY;}
      set{m_maxY = value;}
    }
    public float minY
    {
      get{return m_minY;}
      set{m_minY = value;}
    }
}
