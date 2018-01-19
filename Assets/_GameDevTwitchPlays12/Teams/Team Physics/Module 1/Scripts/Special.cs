using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special  : MonoBehaviour, ISpecial
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

    private PlayerCharacter _playerCharacter;

    public PlayerCharacter m_playerCharacter
    {
        get { return _playerCharacter; }
        set { _playerCharacter = value; }
    }


    private e_specialType _typeSpecial;

    public e_specialType m_typeSpecial
    {
        get { return _typeSpecial; }
        set { _typeSpecial = value; }
    }


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
