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


    public void ResetInventory()
    {
        for (int i = itemPanelParent.childCount - 1; i >= 0; i--)
        {
            var panel = itemPanelParent.GetChild(i).GetComponent<ItemPanelPrefab>();
            if (panel == null) continue;

            panel.Outit();
            panel.Release();
        }

        for (int i = 0; i < inventoryManager.itemList.Count; i++)
        {
            Item itemSO = inventoryManager.itemList[i];
            int count = inventoryManager.itemDic[itemSO];

            ItemPanelPrefab panel = itemPanelPool.GetPooled() as ItemPanelPrefab;

            panel.transform.SetParent(itemPanelParent, false);

            panel.Init(itemSO, count);
        }

    }



}
