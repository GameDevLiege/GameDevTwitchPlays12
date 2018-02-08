using UnityEngine;
using UnityEngine.UI;

public class UIWins : MonoBehaviour {

    public GameObject m_Helmet;
    public Text m_TeamName;
    public Canvas m_CanvasUIWins;

    private static GameObject Helmet;
    private static Text TeamName;
    private static Canvas CanvasUIWins;

    public void Awake()
    {
        Helmet = m_Helmet;
        TeamName = m_TeamName;
        CanvasUIWins = m_CanvasUIWins;
    }

    public UIWins(string teamName, Color teamColor)
    {
        teamColor.a = 100;
        Helmet.GetComponent<Renderer>().material.color = teamColor;
        TeamName.text = teamName;
        TeamName.color = teamColor;
    }

    public void ActiveUIWins(bool activation)
    {
        CanvasUIWins.gameObject.SetActive(activation);
    }
}
