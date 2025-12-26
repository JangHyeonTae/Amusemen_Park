using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    public ItemPanelPrefab itemPanel;
    InventoryManager inventoryManager;
    private ObjectPool itemPanelPool;
    [SerializeField] private Transform itemPanelParent;


    private void OnEnable()
    {
        inventoryManager = InventoryManager.Instance;

        InventoryManager.Instance.OnChanageItem += ResetInventory;
    }

    private void OnDisable()
    {
        InventoryManager.Instance.OnChanageItem -= ResetInventory;
        inventoryManager = null;
    }

    private void Start()
    {
        itemPanelPool = new ObjectPool(itemPanel, 15, itemPanelParent, false);
    }

    private void ShowItem()
    {
        
    }

    public void ResetInventory()
    {

        for (int i = 0; i < inventoryManager.itemDic.Count; i++)
        {
            ItemPanelPrefab item = (ItemPanelPrefab)itemPanelPool.poolList[i];
            item.Outit(inventoryManager.itemList[i]);
            itemPanelPool.poolList[i].Release();
        }

        for (int i = 0; i < inventoryManager.itemDic.Count; i++)
        {
            ItemPanelPrefab panel = itemPanelPool.GetPooled() as ItemPanelPrefab;
            panel.Init(inventoryManager.itemList[i], inventoryManager.itemDic[inventoryManager.itemList[i]]);
        }

    }



}
