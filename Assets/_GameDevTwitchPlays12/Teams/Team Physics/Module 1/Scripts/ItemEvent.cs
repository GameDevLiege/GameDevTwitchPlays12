using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEvent
{
    private static ItemPickup m_onItemPickup;
    public static void AddListener(ItemPickup specialPickup)
    {
        m_onItemPickup += specialPickup;
          
    }
    public static void RemoveListener(ItemPickup specialPickup)
    {
        m_onItemPickup -= specialPickup;

    }
    public static void NotifyNewItem(Item item) {
        m_onItemPickup(item);
    }
    public delegate void ItemPickup(Item item);

}

