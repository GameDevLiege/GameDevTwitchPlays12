using UnityEngine;


public class TimerAPI
{
    public delegate void MyTimer(bool end);
    public delegate void MyTerritoryTimer(bool endLock);
    public delegate void MyEndGameTimer(bool endGame);
    public delegate void MyEndGameGlassTimer(bool endGame);
    private static MyTimer m_onTimer;
    private static MyTerritoryTimer m_onTerritoryTimer;
    private static MyEndGameTimer m_onEndGameTimer;
    private static MyEndGameGlassTimer m_onEndGameGlassTimer;
    #region Listener
    public static void AddTimerListener(MyTimer timer)
    {
        m_onTimer += timer;

    }
    public static void RemoveTimeListener(MyTimer timer)
    {
        m_onTimer -= timer;

    }

    public static void AddTerritoryTimerListener(MyTerritoryTimer timer)
    {
        m_onTerritoryTimer += timer;

    }
    public static void RemoveTerritoryTimerListener(MyTerritoryTimer timer)
    {
        m_onTerritoryTimer -= timer;

    }

    public static void AddEndGameTimerListener(MyEndGameTimer timer)
    {
        m_onEndGameTimer += timer;

    }
    public static void RemoveEndGameTimeListener(MyEndGameTimer timer)
    {
        m_onEndGameTimer -= timer;

    }
    public static void AddEndGameGlassTimerListener(MyEndGameGlassTimer timer)
    {
        m_onEndGameGlassTimer += timer;

    }
    public static void RemoveEndGameGlassTimeListener(MyEndGameGlassTimer timer)
    {
        m_onEndGameGlassTimer -= timer;

    }
    #endregion
    #region Notify
    public static void NotifyEndTimer()
    {
        if (m_onTimer !=null)
        m_onTimer(true);
    }

    public static void NotifyEndGame()
    {
        m_onEndGameTimer(true);
    }

    public static void NotifyEndGameGlass()
    {
        m_onEndGameGlassTimer(true);
    }
    public static void NotifyTerritoryUnlock()
    {
        m_onTerritoryTimer(true);
    }


    #endregion


    public static void LaunchTimer(float seconds, Timer timer)
    {
        timer.LoadTimer(seconds, NotifyEndTimer);
    }

    public static void LaunchGameTimer(float seconds, Timer timer)
    {

        timer.LoadTimer(seconds, NotifyEndGame);

    }

    public static void LaunchGlassTimer(float seconds, Timer timer)
    {

        timer.LoadTimer(seconds, NotifyEndGameGlass);
    }
    public static void LaunchTerritoryLockTimer(float seconds, Timer timer)
    {
        timer.LoadTimer(seconds, NotifyTerritoryUnlock);
    }



}


