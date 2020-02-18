using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Count_up_Timer : MonoBehaviour
{
    public Text text_time;
    public float time;
    private float speed = 1;
    void Start()
    {
        text_time = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        string hours = Mathf.Floor((time % 216000) / 3600).ToString("00");
        string minutes = Mathf.Floor((time % 3600) / 60).ToString("00");
        string seconds = (time % 60).ToString("00.00");
        //string milliseconds = ((time * 1000)%1000).ToString("f2");
        text_time.text = hours + ":" + minutes + ":" + seconds;
    }
}