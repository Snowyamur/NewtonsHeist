using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoilTimer : MonoBehaviour
{
    public GameObject coil;

    bool active = true;
    float timer = 5f;

    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            return;
        }
        active = !active;

        coil.SetActive(active);

        timer = 5f;
    }
}
