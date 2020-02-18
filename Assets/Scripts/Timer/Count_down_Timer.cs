using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Count_down_Timer : MonoBehaviour
{
    public Text text_time;
    public float hours;
    public float minutes;
    public float seconds;
    public float time;
    private float speed = 1;
    void Start()
    {
        text_time = GetComponent<Text>();
        time = (hours * 3600) + (minutes * 60) + seconds;

    }

    // Update is called once per frame
    void Update()
    {

        if (time <= 0)
        {
            time = 0f;
            text_time.text = "00:00:00:00";
        }
        else
        {
            time -= Time.deltaTime;
            string hr = Mathf.Floor((time % 216000) / 3600).ToString("00");
            string min = Mathf.Floor((time % 3600) / 60).ToString("00");
            string sec = (time % 60).ToString("00.00");
            text_time.text = hr + ":" + min + ":" + sec;
        }

    }
}