using System;
using UnityEngine;


public class Timer : MonoBehaviour
{
    public float timer;

    Action actionComplete;

    private float finishTime;
    public float MyTime
    {
        get { return finishTime; }
        set { finishTime = value; }
    }
    private bool isRunning = false;

    public void LoadTimer(float finishTime, Action action)
    {
        this.actionComplete = action;
        timer = 0.0f;
        this.finishTime = finishTime;
        isRunning = false;
    }
    public void StartTimer()
    {
        isRunning = true;
    }
    public void RestartTimer()
    {
        isRunning = true;
        timer = 0.0f;
    }
    public void PauseTimer()
    {

        isRunning = false;

    }

    private void Update()
    {
        if (isRunning)
        {
            timer += Time.deltaTime;


        }
        if (timer >= finishTime && timer != 0.0f)
        {
            timer = 0.0f;
            isRunning = false;
            if (actionComplete != null)
                actionComplete();
        }
    }



}