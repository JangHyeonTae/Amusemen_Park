using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleItem : PooledObject, ISell
{
    // 테스트용 public
    public Item itemSO;
    private GameObject obj;
    public int price;

    public void Init(Item item)
    {
        itemSO = item;
        obj = Instantiate(itemSO.itemVisual);
        obj.transform.parent = this.transform;
        price = (int)(GameSystemManager.Instance.currentStage * 0.1f * item.cost);
    }

    public void GetItem()
    {
        InventoryManager.Instance.AddItem(this);
    }

    public void Use(Item item)
    {
        if(item.canUse)
        {
            InventoryManager.Instance.UseItem(this);
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
