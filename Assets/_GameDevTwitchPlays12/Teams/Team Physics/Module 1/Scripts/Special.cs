using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special  : MonoBehaviour
{

    #region Public Members
    public enum e_specialType
    {
        PEBBLE,
        COINBOX,
        GRENADES,
        SHOVEL,
        PARCHEMENT,
        STRAIN,
        GLASSES
    }
    public e_specialType m_typeSpecial;
    #endregion


    #region Public Void
    public void ChooseTypeOfSpecial(e_specialType special)
    {
        m_typeSpecial = special;
    }
    public void GetItemOrEffect()
    {
        switch(m_typeSpecial)
        {
            case e_specialType.COINBOX:

                break;
            case e_specialType.GLASSES:

                break;
            case e_specialType.GRENADES:

                break;
            case e_specialType.PARCHEMENT:

                break;
            case e_specialType.PEBBLE:

                break;
            case e_specialType.SHOVEL:

                break;
            case e_specialType.STRAIN:

                break;
        }
    }
    #endregion


    #region System
    

    #endregion

    #region Private Void

    #endregion

    #region Tools Debug And Utility

    #endregion


    #region Private And Protected Members

    #endregion

}
