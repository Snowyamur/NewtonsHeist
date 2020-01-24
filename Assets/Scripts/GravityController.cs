using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GravityController : MonoBehaviour
{
    enum GravityDirection
    {
        Down,
        Up,
        Right,
        Left
    }
    GravityDirection m_gravDir;

    public void ChangeGravity(bool multiDir)
    {
        if(!multiDir)
        {
            if(m_gravDir == GravityDirection.Down) //If current direction is down, make up
            {
                Physics2D.gravity = new Vector3(0, 9.81f, 0);
            }
            else if(m_gravDir == GravityDirection.Up) //If current direction is up, make down
            {
                Physics2D.gravity = new Vector3(0, -9.81f, 0);
            }
            return;
        }
        //MULTIDIRECTIONAL GRAVITY
    }
}
