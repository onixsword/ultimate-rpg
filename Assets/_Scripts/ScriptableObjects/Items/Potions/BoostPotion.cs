using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Boost Potion", menuName = "Item/Potion/BoostPotion", order = 1)]
public class BoostPotion : Item {

    [SerializeField]
    private string StatBoost;
    [SerializeField]
    private int BoostValue;

    public string StatBoost1
    {
        get
        {
            return StatBoost;
        }

        set
        {
            StatBoost = value;
        }
    }

    public int BoostValue1
    {
        get
        {
            return BoostValue;
        }

        set
        {
            BoostValue = value;
        }
    }
}
