using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct Condition
{
    public Item preReqItem;
    public int NumberOfTimes;

    public ConditionLogic conditionLogic;

    public delegate bool Comparison(int a, int b);

    public static Dictionary<ConditionLogic, Comparison> conditionMap = new Dictionary<ConditionLogic, Comparison>(){
        {ConditionLogic.Greater, (a,b)=>a>b},
        {ConditionLogic.Equal, (a,b)=>a==b},
        {ConditionLogic.Less, (a,b)=>a<b},
    };
    public bool VerifyCondition(int itensCount)
    {
        return conditionMap[conditionLogic](itensCount, NumberOfTimes);
    }

}

public enum ConditionLogic
{
    Greater,
    Equal,
    Less,
}

public enum ConditionType
{
    Or,
    And,
}

public enum ItemType
{
    Unique,
    Addictive,
    Repeatable,
}

public enum AdultInfluence
{
    Normal,
    Empresario,
    Hippie,
    Punk,
}

public enum OldInfluence
{
    Normal,
    Atleta,
    Cadeirante,
    Zen,
}

[CreateAssetMenu(fileName = "new Item", menuName = "ScriptableObj/Item")]
public class Item : ScriptableObject
{
    public GameObject visual;
    public ItemType itemType;
    public int id;
    public string itemName;
    public float minAge;
    public float maxAge;
    public AdultInfluence adultInfluence;
    public OldInfluence oldInfluence;
    public ConditionType conditionType;
    public List<Condition> conditionList;

    [Header("Adult Influences")]
    public int entrepreneurInfluence;
    public int hippieInfluence;
    public int punkInfluence;

    [Header("Old Influences")]
    public int atleteInfluence;
    public int wheelchairInfluence;
    public int zenInfluence;
}
