using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Money
{
    questMoney,
    normalMoney
}

public class InventoryManager : Singleton<InventoryManager>
{
    public int myMoney = 0;

    protected void Awake()
    {
        base.Awake();

        //파이어베이스 연동
    }
}
