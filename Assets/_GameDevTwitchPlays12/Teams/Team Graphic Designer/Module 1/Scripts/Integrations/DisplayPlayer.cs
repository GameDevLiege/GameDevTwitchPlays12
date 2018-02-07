/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayer : MonoBehaviour {

    public Text[] m_UINamePlayerTourtaupes = new Text[5];

    private static Text[] UINamePlayerTourtaupes = new Text[5];


    public static void DisplayPlayerNameInUI(List<PlayerClass> listPlayer)
    {
        int tourtaupes = 0;
        foreach(PlayerClass player in listPlayer)
        {
            if( player.Team == "Tourtaupes")
            {
                if(tourtaupes < UINamePlayerTourtaupes.Length)
                {
                    UINamePlayerTourtaupes[tourtaupes].text = player.Name;
                    tourtaupes++;
                }
            }

            if (player.Team == "Portemantaupes")
            {
                if (tourtaupes < UINamePlayerTourtaupes.Length)
                {
                    UINamePlayerTourtaupes[tourtaupes].text = player.Name;
                    tourtaupes++;
                }
            }
        }
    }


    private void Start()
    {
        UINamePlayerTourtaupes = m_UINamePlayerTourtaupes;
    }
}
*/