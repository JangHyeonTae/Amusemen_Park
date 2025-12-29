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

    private bool initialized;

    private async void OnEnable()
    {
        inventoryManager = InventoryManager.Instance;
        InventoryManager.Instance.OnChanageItem += ResetInventory;
        itemPanelParent = transform.Find("InventoryView");
        //itemPanelParent = GameObject.FindGameObjectWithTag("InventoryView").transform;
        if (itemPanelParent == null)
        {
            Debug.LogError("InventoryView null");
            return;
        }

        var go = await DataManager.Instance.GetData("ItemPanelPrefab");
        if (go == null)
        {
            Debug.LogError("ItemPanelPrefab 로드 실패");
            return;
        }

        itemPanel = go.GetComponent<ItemPanelPrefab>();
        itemPanelPool = new ObjectPool(itemPanel, 15, itemPanelParent, false);

        initialized = true;
        ResetInventory();
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
        List<ItemPanelPrefab> activePanels = new();

        for (int i = 0; i < activePanels.Count; i++)
        {
            if (activePanels[i] == null) continue;
            activePanels[i].Outit();
            activePanels[i].Release();
        }
        activePanels.Clear();

        for (int i = 0; i < inventoryManager.itemList.Count; i++)
        {
            Item itemSO = inventoryManager.itemList[i];
            int count = inventoryManager.itemDic[itemSO];

            ItemPanelPrefab panel = itemPanelPool.GetPooled() as ItemPanelPrefab;
            panel.transform.SetParent(itemPanelParent, false);
            panel.Init(itemSO, count);

            activePanels.Add(panel);
        }

    }



}
