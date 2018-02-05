using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAPI
{
    private static SpecialPickup m_onSpecialPickup;
    public static void AddListener(SpecialPickup specialPickup)
    {
        m_onSpecialPickup += specialPickup;
          
    }
    public static void RemoveListener(SpecialPickup specialPickup)
    {
        m_onSpecialPickup -= specialPickup;

    }
    public static void NotifyNewSpecial(ISpecial special) {
        m_onSpecialPickup(special);
    }
    public delegate void SpecialPickup(ISpecial special);
}
