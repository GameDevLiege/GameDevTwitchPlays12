﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DidzNeil.ChatAPI 
{

    public interface IMessage
    {
       string GetUserName();
        long GetTimestamp();
        DateTime GetDate();
        string GetMessage();
        Platform GetPlatform();
    }

    public class Message : IMessage
    {
        [SerializeField]
        private string m_userName;

        [SerializeField]
        private string m_message;

        [SerializeField]
        private long m_timestamp;

        [SerializeField]
        private Platform m_platform;

        public Message(string userName, string message, long timestamp, Platform platform)
        {
            m_userName = userName;
            m_message = message;
            m_timestamp = timestamp;
            m_platform = platform;
        }

        public DateTime GetDate()
        {
            return CreateFromTimestamp(m_timestamp);
        }

        public string GetMessage()
        {
            
            return m_message;

        }

        public Platform GetPlatform()
        {
            return m_platform;
        }

        public long GetTimestamp()
        {
            return m_timestamp;
        }


        public string GetUserName()
        {
            return m_userName;
        }

        private DateTime CreateFromTimestamp (long timestamp)
        {
            DateTime timeStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            long time = (long)(timestamp * TimeSpan.TicksPerSecond);
            return new DateTime(timeStart.Ticks + time, System.DateTimeKind.Utc);

        }

        public static long GetCurrentTimeUTC()
        {

            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
          return  (DateTime.Now.ToUniversalTime() - unixStart).Ticks;
        }
    }

    public enum Platform : int
    {
        Unknown = -1,

        Mockup = 0,
        Twitch = 1,
        Facebook = 2,
        Discord=3

    }
}