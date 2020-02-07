using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Throwable : MonoBehaviour
{
    public string name = "Grenade";
    public int id = 0;
    public float speed = 1000f;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
