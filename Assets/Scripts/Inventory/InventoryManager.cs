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
    public Dictionary<Item, int> itemDic;
    public List<Item> itemList;


    public int myQuestPoint = 0;
    public int myNormalPoint = 0;
    public int currentWeight;
    public int maxWeight = 15;

    public Action OnChanageItem;

    protected override void Awake()
    {
        base.Awake();
        itemDic = new();
        itemList = new();
    }


    public int CurrentPoint()
    {
        return myNormalPoint;
    }

    public void UseItem(Item itemSO)
    {
        if (itemDic.ContainsKey(itemSO))
        {
            Item data = itemSO;
            if (itemDic[itemSO] > 1)
            {
                itemDic[itemSO] -= 1;
                
            }
            else
            {
                itemList.Remove(itemSO);
                itemDic.Remove(itemSO);
            }

            currentWeight -= itemSO.weight;
            OnChanageItem?.Invoke();
        }
    }

    public bool AddItem(Item itemSO)
    {
        int bag = currentWeight + itemSO.weight;

        if (bag > maxWeight || itemDic.Count >= 15)
        {
            Debug.Log("Max Weight");
            return false;
        }

        if (itemDic.ContainsKey(itemSO))
        {
            itemDic[itemSO]++;
        }
        else
        {
            itemList.Add(itemSO);
            itemDic.Add(itemSO, 1);
        }

        currentWeight += itemSO.weight;
        OnChanageItem?.Invoke();
        return true;
    }

}
