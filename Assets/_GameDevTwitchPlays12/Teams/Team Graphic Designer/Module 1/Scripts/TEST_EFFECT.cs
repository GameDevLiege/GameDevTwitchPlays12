using UnityEngine;

public class TEST_EFFECT : MonoBehaviour {

    [Header("PLAYERS")]
    public GameObject m_player1;
    public GameObject m_player2;
    [Header("EFFECTS")]
    public GameObject m_effect;


    private void OnGUI()
    {
        Transform effect;

        if (GUI.Button(new Rect(new Vector2(0, 0), new Vector2(100f, 50f)), "Glasses For Player 1"))
        {
            effect = Instantiate(m_effect.transform);
            ObjectsFollow.FollowCharacter(effect, m_player1.transform.position);
        }
        if (GUI.Button(new Rect(new Vector2(0, 50f), new Vector2(100f, 50f)), "Glasses For Player 2"))
        {
            effect = Instantiate(m_effect.transform);
            ObjectsFollow.FollowCharacter(effect, m_player2.transform.position);
        }
    }
}
