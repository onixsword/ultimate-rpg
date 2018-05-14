using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData {

    [SerializeField]
    string playerName;

    [SerializeField]
    int actualScene;
    [SerializeField]
    float actualPositionX;
    [SerializeField]
    float actualPositionY;
    [SerializeField]
    float actualPositionZ;


    [SerializeField]
    int maxHealth;
    [SerializeField]
    int actualHealth;
    [SerializeField]
    int maxMana;
    [SerializeField]
    int actualMana;

    //stats
    [SerializeField]
    int level;
    [SerializeField]
    int streight;
    [SerializeField]
    int dextery;
    [SerializeField]
    int agility;
    [SerializeField]
    int inteligence;
    [SerializeField]
    int luck;
    [SerializeField]
    int vitality;

    public GameData(string playerName, int actualScene, Vector3 actualPosition, int maxHealth, int actualHealth,
        int maxMana, int actualMana, int level, int streight, int dextery, int agility, int inteligence, int luck, int vitality)
    {
        this.playerName = playerName;
        this.actualScene = actualScene;
        this.actualPositionX = actualPosition.x;
        this.actualPositionY = actualPosition.y;
        this.actualPositionZ = actualPosition.z;
        this.maxHealth = maxHealth;
        this.actualHealth = actualHealth;
        this.maxMana = maxMana;
        this.actualMana = actualMana;
        this.level = level;
        this.streight = streight;
        this.dextery = dextery;
        this.agility = agility;
        this.inteligence = inteligence;
        this.luck = luck;
        this.vitality = vitality;
    }
}
