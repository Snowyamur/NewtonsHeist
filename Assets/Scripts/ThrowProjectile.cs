using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowProjectile : MonoBehaviour
{
    public GameObject grenade;

    [SerializeField] float speed = 1000f;
    [SerializeField] GameObject grenPosRight;
    [SerializeField] GameObject grenPosLeft;

    PlayerMechanics mechanics;
    GameObject cloneGrenade;

    void Awake()
    {
        grenPosRight = GameObject.Find("GrenadePointRight");
        grenPosLeft = GameObject.Find("GrenadePointLeft");
        mechanics = GetComponent<PlayerMechanics>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(mechanics.isFacingLeft)
            {
                cloneGrenade = Instantiate(grenade, grenPosLeft.transform.position, grenPosLeft.transform.rotation);
                cloneGrenade.GetComponent<Rigidbody2D>().AddForce(-grenPosLeft.transform.right*1000f, ForceMode2D.Force);
            }
            else
            {
                cloneGrenade = Instantiate(grenade, grenPosRight.transform.position, grenPosRight.transform.rotation);
                cloneGrenade.GetComponent<Rigidbody2D>().AddForce(grenPosRight.transform.right*1000f, ForceMode2D.Force);
            }

        }
    }

    /*public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag != "Player")
        {
            Destroy(gameObject);
        }
    }*/
}
