using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleItem : MonoBehaviour, ISell
{
    // 테스트용 public
    private Item itemSO;
    private GameObject obj;

    public void Init(Item item, Transform parent)
    {
        itemSO = item;
        obj = Instantiate(itemSO.itemVisual);
        obj.transform.parent = parent;
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
        if(item.moneyType == EMoney.questPoint)
        {
            InventoryManager.Instance.myQuestPoint += item.cost;
        }
        else
        {
            InventoryManager.Instance.myNormalPoint += item.cost;
        }
    }


}
