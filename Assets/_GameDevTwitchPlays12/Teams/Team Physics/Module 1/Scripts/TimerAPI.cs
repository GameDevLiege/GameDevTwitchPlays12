using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Homebrew;


public class TimerAPI : MonoBehaviour
{
    public delegate void MyTimer(bool end,Faction faction);
    private static MyTimer m_onTimer;


    public static void AddListener(MyTimer timer)
    {
        m_onTimer += timer;

    }
    public static void RemoveListener(MyTimer timer)
    {
        m_onTimer -= timer;

    }


    public static void NotifyEndTimer()
    {
        m_onTimer(true,null);
    }
   
    public static Timer LaunchTimer(float seconds ) {

        Timer timer = new Timer(seconds, NotifyEndTimer);
        
        return timer;
    }
    

    


}

