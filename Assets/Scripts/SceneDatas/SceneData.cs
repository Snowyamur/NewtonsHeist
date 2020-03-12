using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Holds a scene's data
[System.Serializable]
public class SceneData : MonoBehaviour
{
    //Scene name to transfer to
    protected string scene;
    protected int nextScene;

    //Player's beginning position in new scene
    protected float m_xPos;
    protected float m_yPos;

    //Scene's boundaries
    protected float m_maxX;
    protected float m_minX;
    protected float m_maxY;
    protected float m_minY;

    public virtual string getScene() {return scene;}

    public virtual int getNextScene() {return nextScene;}

    public virtual float xPos
    {
      get{return m_xPos;}
    }
    public virtual float yPos
    {
      get{return m_yPos;}
    }
    public virtual float maxX
    {
      get{return m_maxX;}
    }
    public virtual float minX
    {
      get{return m_minX;}
    }
    public virtual float maxY
    {
      get{return m_maxY;}
    }
    public virtual float minY
    {
      get{return m_minY;}
    }
}
