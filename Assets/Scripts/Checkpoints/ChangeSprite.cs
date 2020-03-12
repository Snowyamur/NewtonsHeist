using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSprite : MonoBehaviour
{
    SpriteRenderer checkSprite;

    public Sprite newSprite;

    void Start()
    {
        checkSprite = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.transform.tag == "Player")
        {
            checkSprite.sprite = newSprite;
        }
    }
}
