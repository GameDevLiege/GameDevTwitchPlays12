﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    #region Public Members
    //public int m_amouthaddedGoldFromChest=50;// check with designers
    public float goldValue = 0;

    public enum e_itemType
    {
        GLASSES = 0,
        COINCHEST = 1,
        GRENADES = 2,
        SHOVEL = 3,
        PARCHEMENT = 4,
        STRAIN = 5

    }
    public enum e_effectType
    {
        INSTANT = 1,
        INVENTORY = 2

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
            case e_itemType.COINCHEST:
                m_effectType = e_effectType.INSTANT;
                break;
            case e_itemType.GRENADES:
                m_effectType = e_effectType.INVENTORY;
                break;
            case e_itemType.SHOVEL:
                m_effectType = e_effectType.INVENTORY;
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
