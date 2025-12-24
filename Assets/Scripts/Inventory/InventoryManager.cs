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
    public Dictionary<SampleItem, int> itemDic;
    public List<SampleItem> itemList;

    public int myQuestPoint = 0;
    public int myNormalPoint = 0;
    private int currentWeight;
    private int maxWeight;

    protected void Awake()
    {
        base.Awake();

    }

    private void Start()
    {
        itemDic = new();
        itemList = new();
    }

    public int CurrentPoint()
    {
        return myNormalPoint;
    }

    public void UseItem(SampleItem item)
    {
        if (itemDic.ContainsKey(item))
        {
            if (itemDic[item] > 1)
            {
                itemDic[item] -= 1;
                
            }
            else
            {
                itemList.Remove(item);
                itemDic.Remove(item);
            }

            currentWeight -= item.itemSO.weight;
        }
    }

    public void AddItem(SampleItem item)
    {
        int bag = currentWeight + item.itemSO.weight;
        if (bag <= maxWeight && itemDic.Count <= 15)
        {
            if (itemDic.ContainsKey(item))
            {
                itemDic[item]++;
            }
            else
            {
                itemList.Add(item);
                itemDic.Add(item, 1);
            }
            currentWeight += item.itemSO.weight;
        }
        else
        {
            Debug.Log("Max Weight");
        }
    }

    
}
