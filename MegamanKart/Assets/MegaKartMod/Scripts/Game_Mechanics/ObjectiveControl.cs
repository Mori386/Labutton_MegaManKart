using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveControl : MonoBehaviour
{
    TextMeshProUGUI timerText;
    float minutes;
    float seconds;

    private void Awake()
    {
        minutes = 0;
        seconds = 0;
        timerText = GameObject.Find("GameManager").transform.Find("GameHUD").Find("HUD").Find("Timer").GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        setUpTimer();
        startTimer();
    }
    private void startTimer()
    {
        StartCoroutine(timerUpdate());
    }
    IEnumerator timerUpdate()
    {
        while (true)
        {
            seconds++;
            if(seconds == 60)
            {
                minutes++;
                seconds = 0;
            }
            setUpTimer();
            yield return new WaitForSeconds(1);
        }
    }
    private void setUpTimer()
    {
        string text;
        text = minutes.ToString();
        text += ":";
        if (seconds < 10)
        {
            text += "0" + seconds.ToString();
        }
        else text += seconds.ToString();
        timerText.text = text;
    }
}
