using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public int passPoint;

    public LayerMask targetLayer;

    private bool IsPass(int point)
    {
        if (InventoryManager.Instance.myNormalPoint < passPoint)
        {
            Debug.Log("실패");
            return false;
        }
        else
        {
            Debug.Log("통과");
            InventoryManager.Instance.myNormalPoint -= passPoint;
            return true;
        }
    }

    private void CheckObj()
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
