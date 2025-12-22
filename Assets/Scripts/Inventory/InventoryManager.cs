using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum EMoney
{
    questPoint,
    normalPoint
}

public class InventoryManager : Singleton<InventoryManager>
{
    public int myQuestPoint = 0;
    public int myNormalPoint = 0;
    public Dictionary<Item, int> itemDic;

    protected void Awake()
    {
        base.Awake();

        //파이어베이스 연동
    }

    private void Start()
    {
        itemDic = new();
    }

    public int CurrentPoint()
    {
        return myNormalPoint;
    }

    public void UseItem(Item item)
    {
        if (itemDic.ContainsKey(item))
        {
            if (itemDic[item] > 1)
            {
                itemDic[item] -= 1;
            }
            else
            {
                itemDic.Remove(item);
            }
        }
    }

    public void AddItem(Item item)
    {
        if (itemDic.ContainsKey(item))
        {
            itemDic[item]++;
        }
        else
        {
            itemDic.Add(item, 1);
        }
    }
}
