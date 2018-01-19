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

        public static int MaxMessageArchived=30;

        private static void ListenToMessageSended(Message message)
        {
            _lastMessages.Enqueue(message);
            if (_lastMessages.Count >= MaxMessageArchived)
                _lastMessages.Dequeue();
        }

        private static Queue<Message> _lastMessages = new Queue<Message>();
        #endregion

        public static bool IgnoreMockUp;
        private static MessageReceived m_onMessageReceived;
       // public delegate void MessageSended(Message message);
       // private static MessageSended m_onMessageSended;

        public static void AddListener(MessageReceived messageReceived)
        {
            m_onMessageReceived += messageReceived;

        }

        public static IEnumerable<Message> GetLastMessages(int nombreDeMessages)
        {
            return _lastMessages.OrderByDescending(p=>p.GetTimestamp()).Take(nombreDeMessages).ToArray();
            //grâce à la bibli Linq
        }

        public static void RemoveListener(MessageReceived messageReceived)
        {
            m_onMessageReceived -= messageReceived;

        }

        public static void NotifyNewMessageToListeners(Message messageReceived)
        {
            if(messageReceived.GetPlatform() == Platform.Mockup && IgnoreMockUp)
            {
                return;
            }

            if (m_onMessageReceived != null)
                m_onMessageReceived(messageReceived);

        }


        #region SEND INFO
        // public delegate void SendMessageToAllOnPlatform(Platform platform, Message msg);



        public static SendMessageTo _sendMessageToUser;
        public static SendMessageToAll _sendMessageEveryBody;

        public static void AddGameToServerListener(SendMessageTo user) { _sendMessageToUser += user; }
        public static void AddGameToServerListener(SendMessageToAll allUser) { _sendMessageEveryBody += allUser; }

        public static void SendMessageToUser(string user, Platform platform, Message msg) {

            if (_sendMessageToUser != null)
                _sendMessageToUser(user, platform, msg);

        }
        public static void SendMessageToEveryUsers( Message msg)
        {

            if (_sendMessageEveryBody != null)
                _sendMessageEveryBody( msg);

        }

        #endregion

    }

    public delegate void MessageReceived(Message message);
    public delegate void SendMessageTo(string user, Platform platform, Message msg);
    public delegate void SendMessageToAll(Message msg);

}
