using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Exp Potion", menuName = "Item/Potion/ExpPotion", order = 1)]
public class ExpPotion : Item {

    [SerializeField]
    int recoverValue;

    public int RecoverValue
    {
        get
        {
            return recoverValue;
        }

        set
        {
            recoverValue = value;
        }
    }
}
