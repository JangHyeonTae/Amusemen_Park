using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanelPrefab : PooledObject
{
    private Item itemSO;
    private SampleItem sampleItem;
    [SerializeField] private Image toolImage;
    [SerializeField] private Button showTool;
    [SerializeField] private Button sellButton;
    [SerializeField] private Button useButton;

    private bool value;
    
    public void Init(SampleItem sampleItem)
    {
        this.itemSO = sampleItem.itemSO;
        showTool.onClick.AddListener(ShowTool);
        sellButton.onClick.AddListener(SellItem);
        useButton.onClick.AddListener(UseItem);
    }


    public void Outit(SampleItem sampleItem)
    {
        if (this.itemSO == sampleItem.itemSO)
        {
            this.itemSO = null;
        }
        showTool.onClick.RemoveListener(ShowTool);
        sellButton.onClick.RemoveListener(SellItem);
        useButton.onClick.RemoveListener(UseItem);
        toolImage.gameObject.SetActive(false);
    }


    private void ShowTool()
    {
        value = !value;
        toolImage.gameObject.SetActive(value);
    }

    private void SellItem()
    {
        InventoryManager.Instance.UseItem(sampleItem);
        sampleItem.Sell(sampleItem.itemSO);
    }

    private void UseItem()
    {
        InventoryManager.Instance.UseItem(sampleItem);
        sampleItem.Use(sampleItem.itemSO);
        
    }
}
