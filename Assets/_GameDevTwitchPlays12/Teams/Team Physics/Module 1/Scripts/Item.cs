using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item  :MonoBehaviour ,IItem
{
    
    #region Public Members
    //public int m_amouthaddedGoldFromChest=50;// check with designers

    public enum e_itemType
    {
        GLASSES = 0,
        PEBBLE =1,
        COINCHEST=2,
        GRENADES=3,
        SHOVEL=4,
        PARCHEMENT=5,
        STRAIN=6
        
    }
    public enum e_itemEffectType
    {
        INSTANT = 1,
        INVENTORY = 2
        
    }
    [SerializeField]
    private Player m_player;

    public Player Player
    {
        get { return m_player; }
        set { m_player = value; }
    }

    [SerializeField]
    private e_itemType m_itemType;

    public e_itemType ItemType
    {
        get { return m_itemType; }
        set { m_itemType = value; }
    }
    [SerializeField]
    private e_itemEffectType m_itemEffectType;

    public e_itemEffectType ItemEffectType
    {
        get { return m_itemEffectType; }
        set { m_itemEffectType = value; }
    }


    #endregion


    #region Public Void
    public void ChooseTypeOfItem(e_itemType item)
    {
        m_itemType = item;
    }
    #endregion


    #region System


    #endregion

    #region Private Void

    #endregion

    #region Tools Debug And Utility
    public static int ItemTypeLength() 
    {
        int typeLenght = Enum.GetNames(typeof(e_itemType)).Length;
        return typeLenght ;
    }
    #endregion


    #region Private And Protected Members

    #endregion

}
