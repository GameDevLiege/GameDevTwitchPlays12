using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Homebrew;

public class FactionTimers
{
    public Timer TimerFactionRed =null;
    public Timer TimerFactionBlue=null;
    public Timer TimerFactionGreen=null;
    public Timer TimerFactionYellow=null;
    public float centralTime=60;
    public Faction faction=null;

    FactionTimers()
    {
        TimerAPI.AddListener(GotEvent);
        Territory.AddListener(GotEvent);
    }

    private void GotEvent(bool isCentral,Faction faction=null) {
        switch (faction.FactionColor.ToString())
        {
            case "green":
                if (isCentral)
                {
                    if (!(TimerFactionGreen == null))
                    {
                        TimerFactionGreen.Restart();
                    }
                    else if (TimerFactionGreen == null)
                    {
                        Timer TimerFactionGreen = TimerAPI.LaunchTimer(centralTime);
                    }
                }
                else {
                    TimerFactionGreen.ShutDown();

                }
                

                break;
            case "red":

                if (isCentral)
                {
                    if (!(TimerFactionRed == null))
                    {
                        TimerFactionRed.Restart();
                    }
                    else if (TimerFactionRed == null)
                    {
                        Timer TimerFactionRed = TimerAPI.LaunchTimer(centralTime);
                    }
                }
                else
                {
                    TimerFactionRed.ShutDown();

                }

                break;
            case "blue":
                if (isCentral)
                {
                    if (!(TimerFactionBlue == null))
                    {
                        TimerFactionBlue.Restart();
                    }
                    else if (TimerFactionBlue == null)
                    {
                        Timer TimerFactionBlue = TimerAPI.LaunchTimer(centralTime);
                    }
                }
                else
                {
                    TimerFactionBlue.ShutDown();

                }
                break;
            case "yellow":
                if (isCentral)
                {
                    if (!(TimerFactionYellow == null))
                    {
                        TimerFactionYellow.Restart();
                    }
                    else if (TimerFactionYellow == null)
                    {
                        Timer TimerFactionYellow = TimerAPI.LaunchTimer(centralTime);
                    }
                }
                else
                {
                    TimerFactionYellow.ShutDown();

                }
                break;
        }
    }


    private void StartTimerRed(bool bol)
    {
 

    }
    private void StartTimerBlue(bool bol)
    {

    }
    private void StartTimerYellow(bool bol)
    {


    }
    private void StartTimerGreen(bool bol)
    {

    }
}
