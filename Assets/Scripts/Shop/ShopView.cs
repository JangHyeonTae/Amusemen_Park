using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    [SerializeField] private Button sellButton;
    [SerializeField] private Button checkButton;
    [SerializeField] private Button closeButton;
    public int passPoint;

    public LayerMask targetLayer;

    private void OnEnable()
    {
        sellButton.onClick.AddListener(SellItems);
        checkButton.onClick.AddListener(CheckPass);
        closeButton.onClick.AddListener(CloseShop);
    }

    private void OnDisable()
    {
        sellButton.onClick.RemoveListener(SellItems);
        checkButton.onClick.RemoveListener(CheckPass);
        closeButton.onClick.RemoveListener(CloseShop);
    }

    private void SellItems()
    {
        SellObj();
    }

    //통과인지 체크
    private void CheckPass()
    {
        if (IsPass(InventoryManager.Instance.myNormalPoint))
        {
            Debug.Log("통과");
        }
        else
        {
            Debug.Log("실패");
        }
    }

    private bool IsPass(int point)
    {
        if (InventoryManager.Instance.myNormalPoint < passPoint)
        {
            return false;
        }
        else
        {
            InventoryManager.Instance.myNormalPoint -= passPoint;
            return true;
        }
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


    private void CloseShop()
    {
        gameObject.SetActive(false);
    }

}
