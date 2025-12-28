using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemPanel : MonoBehaviour
{
    Item itemSO;
    public int cost;

    public void Init(Item itemSO)
    {
        this.itemSO = itemSO;
        cost = itemSO.cost;
    }
}
