using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanelPrefab : PooledObject
{
    private Item itemSO;

    [SerializeField] private Image toolPopUp;

    [SerializeField] private Button ItemImageButton;
    [SerializeField] private Button outButton;
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

        ItemImageButton.onClick.AddListener(ShowTool);
        outButton.onClick.AddListener(OutItem);
        useButton.onClick.AddListener(UseItem);

        OnTool += ShowSet;
    }


    public void Outit()
    {
        itemSO = null;

        ItemImageButton.onClick.RemoveListener(ShowTool);
        outButton.onClick.RemoveListener(OutItem);
        useButton.onClick.RemoveListener(UseItem);
        toolPopUp.gameObject.SetActive(false);

        OnTool -= ShowSet;
    }

    private void ShowSet(bool value)
    {
        toolPopUp.gameObject.SetActive(value);
    }

    private void ShowTool()
    {
        ToolValue = !ToolValue;
    }

    private void OutItem()
    {
        SampleItem item = PoolManager.Instance.itemPool.GetPooled() as SampleItem;
        item.Init(itemSO);
        item.transform.position = FindObjectOfType<PlayerController>().transform.position;
        InventoryManager.Instance.UseItem(itemSO);
    }

    private void UseItem()
    {
        InventoryManager.Instance.UseItem(itemSO);
        
    }
}
