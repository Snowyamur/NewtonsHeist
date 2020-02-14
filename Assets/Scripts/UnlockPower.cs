using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class UnlockPower : MonoBehaviour
{
    public string power;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.tag == "Player")
        {
            LevelManager.current.playerData.powers[power] = true;
        }
    }

}
