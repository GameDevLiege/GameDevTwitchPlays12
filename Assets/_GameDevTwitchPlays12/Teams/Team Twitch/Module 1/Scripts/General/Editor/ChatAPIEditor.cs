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
            base.OnInspectorGUI();
            DisplayLastMessages(ChatAPI.GetLastMessages(20));
        }

        private static void DisplayLastMessages(IEnumerable<Message> messages)
        {
            GUILayout.Label("Last 10 Messages");
            //EditorGUILayout.TextArea();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("UserName");
            GUILayout.Label("Messages");
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical();
            foreach (Message info in messages)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(info.GetUserName());
                GUILayout.Label(info.GetMessage());
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();

        }
    }
}