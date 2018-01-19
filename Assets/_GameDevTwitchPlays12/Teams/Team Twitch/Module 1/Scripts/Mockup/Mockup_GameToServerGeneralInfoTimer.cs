using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace DidzNeil.ChatAPI
{
    public class Mockup_GameToServerGeneralInfoTimer : MonoBehaviour
    {
        
        public float _sendTimeEvery = 30;

        private void Start()
        {
            StartCoroutine(SendTimeToAllUsers());
        }

      

        private IEnumerator SendTimeToAllUsers()
        {


            while (true)
            {
                DateTime now = Message.CreateFromTimestamp();
                Message msg = new Message("Game Admin", "Server time: " + now.ToString("hh:mm:ss"), Message.GetCurrentTimeUTC(), Platform.Game);

                ChatAPI.SendMessageToEveryUsers(msg);

                yield return new WaitForSeconds(_sendTimeEvery);
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
