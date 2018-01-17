using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


namespace DidzNeil.ChatAPI
{
    public class ChatAPI
    {
#region LISTEN TO MYSELF
        static ChatAPI()
        {
            AddListener(ChatAPI.ListenToMessageSended);
        }

        private static void ListenToMessageSended(Message message)
        {
            _lastMessages.Enqueue(message);
            if (_lastMessages.Count >= 100)
                _lastMessages.Dequeue();
        }

        private static Queue<Message> _lastMessages = new Queue<Message>();
        #endregion

        public delegate void MessageReceived(Message message);
        private static MessageReceived m_onMessageReceived;

        public static void AddListener(MessageReceived messageReceived)
        {
            m_onMessageReceived += messageReceived;

        }

        public static IEnumerable<Message> GetLastMessages(int nombreDeMessages)
        {
            return _lastMessages.Take(nombreDeMessages).ToArray();
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
