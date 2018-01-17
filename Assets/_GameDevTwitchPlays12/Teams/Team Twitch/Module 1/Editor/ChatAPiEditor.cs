using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DidzNeil.ChatAPI
{
    [CustomEditor(typeof(ChatAPIMono))]
    public class ChatAPIEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DisplayLastMessages(ChatAPI.GetLastMessages(20));
        }

        private static void DisplayLastMessages(IEnumerable<Message> messages)
        {
            GUILayout.Label("Last 10 Messages");
            foreach (Message msg in messages)
            {

                GUILayout.Label(">:"+msg.GetMessage());
            }
        }
    }
}