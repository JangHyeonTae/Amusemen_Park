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

    [SerializeField] private InventoryView inventoryView;

    public int myQuestPoint = 0;
    public int myNormalPoint = 0;
    public int currentWeight;
    public int maxWeight = 15;

    protected override void Awake()
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

    public void UseItem(Item itemSO)
    {
        if (itemDic.ContainsKey(itemSO))
        {
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
        }
    }

    public void AddItem(Item itemSO)
    {
        int bag = currentWeight + itemSO.weight;
        if (bag <= maxWeight && itemDic.Count < 15)
        {
            if (itemDic.ContainsKey(itemSO))
            {
                itemDic[itemSO]++;
                Debug.Log(itemDic[itemSO]);
            }
            else
            {
                itemList.Add(itemSO);
                itemDic.Add(itemSO, 1);
            }
            currentWeight += itemSO.weight;
        }
        else
        {
            Debug.Log("Max Weight");
        }
    }

    public void OpenInventory(bool value)
    {
        inventoryView.gameObject.SetActive(value);
    }
}
