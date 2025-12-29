using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : Singleton<ShopManager>
{
    public GameObject shopView;
    public GameObject shopObj;

    public int passPoint;

    private bool isOpen;
    public bool IsOpen { get  { return isOpen; } set { isOpen = value;  OnOpen?.Invoke(isOpen); } }
    public Action<bool> OnOpen;

    private void OnDisable()
    {
        shopView = null;    
    }

    public void ShowShop(bool value)
    {
        if(shopView == null)
            shopView = FindObjectOfType<ShopView>().transform.Find("ShopView").gameObject;

        if(shopObj == null)
            shopObj = GameObject.Find("Shop");

        shopView.gameObject.SetActive(value);
        IsOpen = value;
    }



}
