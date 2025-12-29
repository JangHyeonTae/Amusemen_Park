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


    private void OnEnable()
    {
        checkButton.onClick.AddListener(CheckPass);
        closeButton.onClick.AddListener(CloseShop);
    }

    private void OnDisable()
    {
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
            GameSystemManager.Instance.ChangeMap();
        }
        else
        {
            player.Die();
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


    private void CloseShop()
    {
        gameObject.SetActive(false);
    }

}
