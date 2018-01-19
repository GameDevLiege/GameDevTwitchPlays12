using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Homebrew;


public class TimerAPITest : MonoBehaviour
{
    public delegate void MyTimer(bool end);
    private static MyTimer m_onTimer;
    public Text timerdisplay;
    public float timer = 1000000000f;
    public Material m_cube;
    public Timer m_timerCubeGreen;

    private void Awake()
    {
        m_cube.color = Color.black;
        m_timerCubeGreen = new Timer(timer, CubeGreen);
    }

    private void Start()
    {
        m_timerCubeGreen.Restart();
        Debug.Log(m_timerCubeGreen.timer);
    }

    public void CubeGreen()
    {
        m_cube.color = Color.green;
    }

    
    public void Display()
    {
        timerdisplay.text = "Timer: " + m_timerCubeGreen.MyTime.ToString();
        
    }




}

