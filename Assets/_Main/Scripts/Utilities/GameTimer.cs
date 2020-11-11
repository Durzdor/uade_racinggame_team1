using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public static GameTimer intance;

    public Text timeCounter;
    public string timePlayingStr;

    private TimeSpan timePlaying;
    private bool timerGoing;
    private float elapsedTime;

    private void Awake()
    {
        intance = this;
    }

    private void Start()
    {
        timeCounter.text = "Time: 00:00:00";
        timerGoing = false;
        BeginTimer();
    }

    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerGoing = false;
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            timePlayingStr = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingStr;
            yield return null;
        }
    }
}