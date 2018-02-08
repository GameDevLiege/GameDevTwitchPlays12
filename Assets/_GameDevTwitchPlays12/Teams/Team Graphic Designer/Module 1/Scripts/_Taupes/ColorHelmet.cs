using UnityEngine;

public class ColorHelmet : MonoBehaviour {

    public GameObject m_HelmetColor;

    public Color m_ColorHelmet = Color.blue;

    private static GameObject HelmetColor;
    private static Color ColorHelmets;

    private void Awake()
    {
        UpdateColor(m_ColorHelmet);
    }

    private void Start()
    {
        HelmetColor = m_HelmetColor;
        ColorHelmets = m_ColorHelmet;
    }

    public void UpdateColor(Color helmetColor)
    {
        Renderer r = HelmetColor.GetComponent<Renderer>();
        if(r != null)
            r.material.color = helmetColor;

        ColorHelmets = helmetColor;
    }
}
