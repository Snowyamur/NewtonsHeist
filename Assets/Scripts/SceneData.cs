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
}
