using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special  : MonoBehaviour
{

    #region Public Members
    public int m_amouthaddedGoldFromChest=50;// check with designers
    


    public enum e_specialType
    {
        PEBBLE,
        COINCHEST,
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
