using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    #region Public Members
    //public int m_amouthaddedGoldFromChest=50;// check with designers

    public enum e_itemType
    {
        GLASSES = 0,
        PEBBLE = 1,
        COINCHEST = 2,
        GRENADES = 3,
        SHOVEL = 4,
        PARCHEMENT = 5,
        STRAIN = 6

    }
    public enum e_effectType
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
    private e_effectType m_effectType;

    public e_effectType EffectType
    {
        get { return m_effectType; }
        set { m_effectType = value; }
    }
    #endregion
    
    #region Public Void
    public void ChooseTypeOfItem(e_itemType type)
    {
        m_itemType = type;
    }
    public void ChooseEffectOfItem(e_effectType effect)
    {
        m_effectType = effect;
    }
    #endregion

    #region Tools Debug And Utility
    public static int ItemTypeLength()
    {
        int typeLenght = Enum.GetNames(typeof(e_itemType)).Length;
        return typeLenght;
    }

    public void ChoseEffectItem(e_itemType type)
    {
        switch (type)
        {
            case e_itemType.GLASSES:
                m_effectType = e_effectType.INVENTORY;
                break;
            case e_itemType.PEBBLE:
                m_effectType = e_effectType.INVENTORY;
                break;
            case e_itemType.COINCHEST:
                m_effectType = e_effectType.INSTANT;
                break;
            case e_itemType.GRENADES:
                m_effectType = e_effectType.INVENTORY;
                break;
            case e_itemType.SHOVEL:
                m_effectType = e_effectType.INSTANT;
                break;
            case e_itemType.PARCHEMENT:
                m_effectType = e_effectType.INSTANT;
                break;
            case e_itemType.STRAIN:
                m_effectType = e_effectType.INSTANT;
                break;
            default:
                break;
        }
    }
    #endregion
}
