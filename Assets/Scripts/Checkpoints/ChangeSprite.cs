using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSprite : MonoBehaviour
{
    SpriteRenderer checkSprite;
    ParticleSystem ps;

    public Sprite newSprite;

    void Start()
    {
        checkSprite = GetComponent<SpriteRenderer>();
        ps = GetComponent<ParticleSystem>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.tag == "Player")
        {
            ParticleSystem.MainModule psMain = ps.main;
            checkSprite.sprite = newSprite;
            psMain.startColor = new Color(0.078f, 0.99f, 0.9f, 1f);
        }
    }
}
