using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class SampleItem : MonoBehaviour, ISell
{
    private Item itemSO;

    public SampleItem(Item item)
    {
        itemSO = item;
    }

    public void GetItem(Item item)
    {
        InventoryManager.Instance.AddItem(item);
    }

    public void Use(Item item)
    {
        if(item.canUse)
        {
            InventoryManager.Instance.UseItem(item);
        }
    }

    public void Sell(Item item)
    {
        if(item.moneyType == EMoney.questMoney)
        {
            InventoryManager.Instance.questMoney += item.cost;
        }
        else
        {
            InventoryManager.Instance.normalMoney += item.cost;
        }
    }


}
