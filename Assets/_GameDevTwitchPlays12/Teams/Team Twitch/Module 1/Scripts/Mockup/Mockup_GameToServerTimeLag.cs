using DidzNeil.ChatAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mockup_GameToServerTimeLag : MonoBehaviour {


    public float _average;
    private float _totalLag;
    private int _userAsking;
    public Message _lastUserAsking;
    public Message _lastMessage;

    // Use this for initialization
    void Start () {

        ChatAPI.AddListener(CheckForUserAskingTime);
    }

    private void CheckForUserAskingTime(Message receivedMessage)
    {
        _lastMessage = receivedMessage;
        string userMessage = receivedMessage.GetMessage();
        //time 16:30:20
        if (userMessage.StartsWith("time"))
        {

            userMessage = userMessage.Substring(4);
            string[] tokens = userMessage.Split(':');

            int hh = 0;
            int mm = 0;
            int ss = 0;
            bool timeParsed = false;
            if (tokens.Length > 1)
            {

                try
                {
                    hh = int.Parse(tokens[0]);
                    mm = int.Parse(tokens[1]);
                    timeParsed = true;
                }
                catch (Exception) { return; }
            }
            if (tokens.Length > 2)
            {
                try
                {
                    ss = int.Parse(tokens[2]);
                    timeParsed = true;
                }
                catch (Exception) { return; }

            }

            if (timeParsed) {
                _lastUserAsking = receivedMessage;
                 SendLagTimeToUser(receivedMessage.GetUserName(), receivedMessage.GetPlatform(), hh, mm, ss);

            }


        }
    }

    public void SendLagTimeToUser(string user, Platform platform, int hours, int minutes, int seconds)
    {
        seconds = Mathf.Clamp(seconds, 0, 59);
        hours = Mathf.Clamp(hours, 0, 23);
        minutes = Mathf.Clamp(minutes, 0, 59);

        DateTime now = Message.CreateFromTimestamp();
        DateTime userTime = new DateTime(now.Year, now.Month, now.Day, hours, minutes, seconds);
        float time = GetTimeBetween(userTime, now);
        string timeLag = "You have " + GetTimeBetween(userTime, now) + " seconds of lag.";

        AddAverageValue(time);
        SendWhisperToUser(user, timeLag);
    }

    private void AddAverageValue(float timeLag)
    {
        _totalLag += timeLag;
        _userAsking++;
        _average = _totalLag / (float) _userAsking;

    }

    private void SendWhisperToUser(string userToWhisper, string theWhisper)
    {

        Message whisper = new Message(userToWhisper, theWhisper, Message.GetCurrentTimeUTC(), Platform.Game);
        ChatAPI.SendMessageToUser(userToWhisper, Platform.Twitch, whisper);

    }
    public static long GetTimeBetween(DateTime past, DateTime now)
    {
        long pastTime = GetTimeFrom(past);
        long nowTime = GetTimeFrom(now);
        return nowTime - pastTime;
    }
    public static long GetTimeFrom(DateTime time)
    {


        DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        return (long)(time.ToUniversalTime() - unixStart).TotalSeconds;
    }
}
