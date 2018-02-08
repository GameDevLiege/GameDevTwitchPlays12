using UnityEngine;

public class ColorHelmet : MonoBehaviour {

    public GameObject m_HelmetColor;

    public Color m_ColorHelmet = Color.blue;

    void Awake()
    {
        UpdateColor(m_ColorHelmet);
    }

    public void UpdateColor(Color helmetColor)
    {
        Renderer r = m_HelmetColor.GetComponent<Renderer>();
        if(r != null)
            r.material.color = helmetColor;

        m_ColorHelmet = helmetColor;
    }
}
