using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    [SerializeField] private Button sellButton;
    [SerializeField] private Button checkButton;
    [SerializeField] private Button closeButton;

    public LayerMask targetLayer;

    private void OnEnable()
    {
        sellButton.onClick.AddListener(SellObj);
        checkButton.onClick.AddListener(CheckPass);
        closeButton.onClick.AddListener(CloseShop);
    }

    private void OnDisable()
    {
        sellButton.onClick.RemoveListener(SellObj);
        checkButton.onClick.RemoveListener(CheckPass);
        closeButton.onClick.RemoveListener(CloseShop);
    }

    //통과인지 체크
    private void CheckPass()
    {
        PlayerController player = FindObjectOfType<PlayerController>();

        if (IsPass(InventoryManager.Instance.myNormalPoint))
        {
            player.transform.position = new Vector3(0, 0, 0);
            GameSystemManager.Instance.ChangeScene("Map");
        }
        else
        {
            player.Die();
        }
    }

    private bool IsPass(int point)
    {
        if (InventoryManager.Instance.myNormalPoint < ShopManager.Instance.passPoint)
        {
            return false;
        }
        else
        {
            InventoryManager.Instance.myNormalPoint -= ShopManager.Instance.passPoint;
            return true;
        }
    }


    private void CloseShop()
    {
        ShopManager.Instance.ShowShop(false);
    }

    private void SellObj()
    {
        Collider[] items = Physics.OverlapSphere(ShopManager.Instance.shopObj.transform.position, 4f, targetLayer);
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
