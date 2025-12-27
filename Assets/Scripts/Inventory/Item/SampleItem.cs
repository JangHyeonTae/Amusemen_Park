using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleItem : PooledObject, ISell
{
    // 테스트용 public
    public Item itemSO;
    private GameObject obj;
    public int price;

    private void OnDisable()
    {
        itemSO = null;
        obj = null;
        price = 0;
    }
    public void Init(Item item)
    {
        itemSO = item;
        obj = Instantiate(itemSO.itemVisual);
        obj.transform.SetParent(this.transform, false);

        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;

        price = (int)(GameSystemManager.Instance.currentStage * 0.1f * item.cost);
    }

    public void GetItem()
    {
        bool added = InventoryManager.Instance.AddItem(itemSO);

        if (added)
        {
            PopUpManager.Instance.HidePopUP();
            this.Release();
        }
        else
        {
            // 못 들감 사운드
        }
    }

    public void Use(Item item)
    {
        if(item.canUse)
        {
            InventoryManager.Instance.UseItem(itemSO);
        }
    }

    public void Sell()
    {
        if(itemSO.moneyType == EMoney.questPoint)
        {
            InventoryManager.Instance.myQuestPoint += itemSO.cost;
            Release();
        }
        else
        {
            InventoryManager.Instance.myNormalPoint += itemSO.cost;
            Release();
        }

        
    }

}
