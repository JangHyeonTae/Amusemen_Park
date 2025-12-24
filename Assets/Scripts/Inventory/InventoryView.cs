using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    public ItemPanelPrefab itemPanel;
    InventoryManager inventoryManager;
    private ObjectPool itemPanelPool;

    

    private void Start()
    {
        inventoryManager = InventoryManager.Instance;
        itemPanelPool = new ObjectPool(itemPanel, 15, this.transform, false);
    }

    private void ShowItem()
    {
        
    }

    private void ReSpawn()
    {
        for(int i =0; i < inventoryManager.itemDic.Count; i++)
        {
            ItemPanelPrefab obj = itemPanelPool.GetPooled() as ItemPanelPrefab;
            obj.Init(inventoryManager.itemList[i]);
        }
    }


}
