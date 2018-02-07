using UnityEngine;

public class TEST_EFFECT : MonoBehaviour {

    public GameObject m_player1;
    public GameObject m_player2;

    public GameObject m_effect;


    private void OnGUI()
    {
        Transform effect = m_effect.transform;

        if (GUI.Button(new Rect(new Vector2(0, 0), new Vector2(100f, 50f)), "Glasses For Player 1"))
            ObjectsFollow.FollowCharacter(effect, m_player1.transform.position);
        if (GUI.Button(new Rect(new Vector2(0, 50f), new Vector2(100f, 50f)), "Glasses For Player 2"))
            ObjectsFollow.FollowCharacter(effect, m_player2.transform.position);
    }
}
