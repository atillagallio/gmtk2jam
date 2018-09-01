using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


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
[CreateAssetMenu(fileName = "new Item", menuName = "ScriptableObj/Item")]
public class Item : ScriptableObject
{

    public GameObject visual;
    public int id;
    public string itemName;
    public ConditionType conditionType;

    public List


}
