using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DidzNeil.ChatAPI { 

    public class Mockup_MessageGenerator : MonoBehaviour
    {

        public float _minTimeSending=0.1f;
        public float _maxTimeSending=1f;

        public List<string> m_usernames;
        public List<string> m_randomMessages;
       
        IEnumerator Start () {

            while (true)
            {
                SendRandomMessage();
                yield return new WaitForSeconds(UnityEngine.Random.Range(_minTimeSending, _maxTimeSending));
            }
	    }

        private void SendRandomMessage()
        {
            Message msg = new Message(
                m_usernames[UnityEngine.Random.Range(0, m_usernames.Count)], m_randomMessages[UnityEngine.Random.Range(0, m_randomMessages.Count)]
                , GetTimestamp(DateTime.Now), Platform.Mockup
                );
            ChatAPI.NotifyNewMessageToListeners(msg);
        }

        public static long GetTimestamp(DateTime dateTime)

        {

            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            return  (dateTime.ToUniversalTime() - unixStart).Ticks;

        }
    }
}