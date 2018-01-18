using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace DidzNeil.ChatAPI
{
    public class Mockup_GameToServer : MonoBehaviour
    {
        public string m_userToSendMessage;
        public string m_message;

        private void Start()
        {
            StartCoroutine(SendTimeToAllUsers());

            ChatAPI.AddListener(CheckForUserAskingTime);
        }

        private void CheckForUserAskingTime(Message receivedMessage)
        {
            string userMessage = receivedMessage.GetMessage();
            //time 16:30:20
            if (userMessage.StartsWith("time")) {
                
                userMessage =  userMessage.Substring(4);
                string[] tokens = userMessage.Split(':');

                int hh = 0;
                int mm = 0;
                int ss = 0;
                bool timeParsed = false;
                if (tokens.Length > 1)
                {
                  
                    try {
                        hh = int.Parse(tokens[0]);
                        mm = int.Parse(tokens[1]);
                        timeParsed = true;
                    }
                    catch (Exception) { return; }
                }
                 if (tokens.Length > 2)
                {
                    try {
                        ss = int.Parse(tokens[2]);
                        timeParsed = true;
                    }
                    catch (Exception) { return; }

                }

                 if(timeParsed)
                    SendLagTimeToUser(receivedMessage.GetUserName(), receivedMessage.GetPlatform(), hh, mm, ss);


            }
        }

        public void SendLagTimeToUser(string user, Platform platform, int hours, int minutes, int seconds)
        {
            DateTime now = Message.CreateFromTimestamp();
            DateTime userTime = new DateTime(now.Year, now.Month, now.Day, hours, minutes, seconds);
            
            string timeLag = "You have " + GetTimeBetween(userTime, now)+" seconds of lag.";
            SendWhisperToUser(user, timeLag);
        }

        private void SendWhisperToUser(string userToWhisper, string theWhisper)
        {
            
                Message whisper = new Message(userToWhisper, theWhisper, Message.GetCurrentTimeUTC(), Platform.Game);
                ChatAPI.SendMessageToUser(userToWhisper, Platform.Twitch, whisper);
            
        }

        private IEnumerator SendTimeToAllUsers()
        {


            while (true)
            {
                DateTime now = Message.CreateFromTimestamp();
                Message msg = new Message("Game Admin", "Time: " + now.ToString("hh:mm:ss"), Message.GetCurrentTimeUTC(), Platform.Game);

                ChatAPI.SendMessageToEveryUsers(msg);

                yield return new WaitForSeconds(5f);
            }
        }


        public static long GetTimeBetween(DateTime past, DateTime now)
        {
            long pastTime = GetTimeFrom(past);
            long nowTime = GetTimeFrom(now);
            return nowTime - pastTime;
        }
        public static long GetTimeFrom(DateTime time) {


            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            return (long)(time.ToUniversalTime() - unixStart).TotalSeconds;
        }


    }
}
