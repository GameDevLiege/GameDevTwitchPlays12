using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DidzNeil.ChatAPI
{
    [CustomEditor(typeof(Mockup_SequenceMessagesEditor))]
    public class Mockup_SequenceMessagesEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
        }

        private static void InputMessages(IEnumerable<Message> messages)
        {

            GUILayout.Label("MockUp Test ");

            //EditorGUILayout.TextArea();
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("UserName");
            GUILayout.Label("Messages");
            EditorGUILayout.EndHorizontal();

      
            EditorGUILayout.BeginVertical();
            foreach (Message info in messages)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.TextField(info.GetUserName());
                GUILayout.TextArea(info.GetMessage());
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();

        }
    }
}

