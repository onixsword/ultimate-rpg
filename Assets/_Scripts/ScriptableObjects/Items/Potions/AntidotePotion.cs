using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Antidote Potion", menuName = "Item/Potion/AntidotePotion", order = 1)]
public class AntidotePotion : Item {

    [SerializeField]
    private string malusToClean;

    public string MalusToClean1
    {
        get
        {
            return malusToClean;
        }

        set
        {
            malusToClean = value;
        }
    }
}
