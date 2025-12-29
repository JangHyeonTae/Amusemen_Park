using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject shopView;
    public LayerMask targetLayer;

    private void OnEnable()
    {
        shopView = GameObject.FindGameObjectWithTag("ShopView");
    }

    private void OnDisable()
    {
        shopView = null;    
    }

    public void ShowShop()
    {
        gameObject.SetActive(true);
    }


    private void SellObj()
    {
        Collider[] items = Physics.OverlapSphere(transform.position, 2f, targetLayer);
        int sum = 0;
        if (items.Length > 0)
        {
            for (int i = 0; i < items.Length; i++)
            {
                SampleItem item = items[i].GetComponent<SampleItem>();
                sum += item.price;
                item.Sell();
            }
        }
    }

}
