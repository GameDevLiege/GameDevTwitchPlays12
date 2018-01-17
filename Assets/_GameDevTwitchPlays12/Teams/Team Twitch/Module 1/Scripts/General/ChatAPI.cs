using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DidzNeil.ChatAPI
{
    public class ChatAPI
    {
        public delegate void MessageReceived(Message message);
        private static MessageReceived m_onMessageReceived;

        public static void AddListener(MessageReceived messageReceived)
        {
            m_onMessageReceived += messageReceived;

        }
        public static void RemoveListener(MessageReceived messageReceived)
        {
            m_onMessageReceived -= messageReceived;

        }


        public static void NotifyNewMessageToListeners(Message messageReceived) {
            if (m_onMessageReceived != null)
                m_onMessageReceived(messageReceived);

        }


    }

}
