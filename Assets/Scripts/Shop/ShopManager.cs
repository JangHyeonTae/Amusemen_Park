using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private ShopView shopView;
    
    public void ShowShop()
    {
        shopView.gameObject.SetActive(true);
    }

}
