using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);

        if (collision.transform.CompareTag("Player"))
        {
            GameManager.Instance.RespawnPlayer();
        }
    }
}
