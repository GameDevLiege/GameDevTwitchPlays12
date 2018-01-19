using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHelmet : MonoBehaviour {

    public GameObject m_HelmetColor;

    public enum e_ColorHelmet{
        YELLOW,
        BLUE,
        GREEN,
        RED
    }
    public e_ColorHelmet m_ColorHelmet = e_ColorHelmet.BLUE;

    void Start()
    {
        switch (m_ColorHelmet)
        {
            case e_ColorHelmet.BLUE:
                m_color = Color.blue;
                break;
            case e_ColorHelmet.YELLOW:
                m_color = Color.yellow;
                break;
            case e_ColorHelmet.GREEN:
                m_color = Color.green;
                break;
            case e_ColorHelmet.RED:
                m_color = Color.red;
                break;
        }
        Renderer r = m_HelmetColor.GetComponent<Renderer>();
        r.material.color = m_color;

    }

    private Color m_color;
}
