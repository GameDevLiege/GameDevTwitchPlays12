using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEvent
{
    private static ItemPickup m_onItemPickup;
    private static ItemUse m_onItemUse;
    public static void AddListener(ItemPickup itemPickup)
    {
        m_onItemPickup += itemPickup;
          
    }
    public static void RemoveListener(ItemPickup specialPickup)
    {
        m_onItemPickup -= specialPickup;

    }
    public static void NotifyNewItem(Item item,Player player) {
        m_onItemPickup(item,player);
    }
    public static void NotifyItemUse() {

        m_onItemUse();
    }

    public delegate void ItemPickup(Item item,Player player);
    public delegate Dictionary<Item,Player> ItemUse();
}

