using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject {

    [SerializeField]
    protected string itemName;
    [SerializeField]
    protected string description;
    [SerializeField]
    protected Sprite image;
    [SerializeField]
    protected int saleValue;
    [SerializeField]
    protected bool keyObject;
    [SerializeField]
    protected int rarity;

    protected string ItemName
    {
        get
        {
            return itemName;
        }

        set
        {
            itemName = value;
        }
    }

    protected string Description
    {
        get
        {
            return description;
        }

        set
        {
            description = value;
        }
    }

    protected Sprite Image
    {
        get
        {
            return image;
        }

        set
        {
            image = value;
        }
    }

    protected int SaleValue
    {
        get
        {
            return saleValue;
        }

        set
        {
            saleValue = value;
        }
    }

    protected bool KeyObject
    {
        get
        {
            return keyObject;
        }

        set
        {
            keyObject = value;
        }
    }
}
