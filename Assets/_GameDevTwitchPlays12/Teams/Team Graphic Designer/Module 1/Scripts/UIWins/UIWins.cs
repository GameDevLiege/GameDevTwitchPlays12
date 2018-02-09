﻿using UnityEngine;
using UnityEngine.UI;

public class UIWins : MonoBehaviour {

    public GameObject m_Helmet;
    public Text m_TeamName;

    private static GameObject Helmet;
    private static Text TeamName;

    public void SetInfo(string teamName, Color teamColor)
    {
        teamColor.a = 100;
        Helmet.GetComponent<Renderer>().material.color = teamColor;
        TeamName.text = teamName;
        TeamName.color = teamColor;
    }

    public void ActiveUIWins(bool activation)
    {
        gameObject.SetActive(activation);
    }

    private void Awake()
    {
        Helmet = m_Helmet;
        TeamName = m_TeamName;
    }
}
