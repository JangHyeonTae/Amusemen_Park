using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanelPrefab : PooledObject
{
    private Item itemSO;

    [SerializeField] private Image toolImage;

    [SerializeField] private Button showTool;
    [SerializeField] private Button sellButton;
    [SerializeField] private Button useButton;

    [SerializeField] private TextMeshProUGUI itemCountText;

    private bool toolValue;
    public bool ToolValue { get { return toolValue; } set { toolValue = value; OnTool?.Invoke(toolValue); } }
    public Action<bool> OnTool;
    
    public void Init(Item itemSO, int itemCount)
    {
        ToolValue = false;
        this.itemSO = itemSO;

        itemCountText.text = itemCount.ToString();

        showTool.onClick.AddListener(ShowTool);
        sellButton.onClick.AddListener(SellItem);
        useButton.onClick.AddListener(UseItem);

        OnTool += ShowSet;
    }


    public void Outit(Item itemSO)
    {
        if (this.itemSO == itemSO)
        {
            this.itemSO = null;
        }

        showTool.onClick.RemoveListener(ShowTool);
        sellButton.onClick.RemoveListener(SellItem);
        useButton.onClick.RemoveListener(UseItem);
        toolImage.gameObject.SetActive(false);

        OnTool -= ShowSet;
    }

    private void ShowSet(bool value)
    {
        toolImage.gameObject.SetActive(value);
    }

    private void ShowTool()
    {
        ToolValue = !ToolValue;
    }

    private void SellItem()
    {
        InventoryManager.Instance.UseItem(itemSO);
    }

    private void UseItem()
    {
        InventoryManager.Instance.UseItem(itemSO);
        
    }
}
