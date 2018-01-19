using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using DidzNeil.ChatAPI;

    [CustomEditor(typeof(Mockup_GameToServer))]
    [CanEditMultipleObjects]
    public class Mockup_GameToServerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Send Message to: ");
            if (GUI.Button(Rect.MinMaxRect(0,0,20,20), "Neil"))
                //GUI.Window(0, new Rect(110, 10, 200, 60),DoMyWindow, "Message to Neil");

           GUILayout.EndHorizontal();
            
        }

        static void DoMyWindow(int windowID)
        {
            if (GUI.Button(new Rect(10, 20, 100, 20), "Hello World"))
                Debug.Log("Got a click in window with color " + GUI.color);

            GUI.DragWindow(new Rect(0, 0, 10000, 10000));
        }
    }

