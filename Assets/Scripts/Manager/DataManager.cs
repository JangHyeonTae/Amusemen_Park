using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class DataManager : Singleton<DataManager>
{

    protected override void Awake()
    {
        base.Awake();
    }

    public GameObject GetData(string name)
    {
        GameObject data = null;
        Addressables.LoadAssetAsync<GameObject>(name).Completed += (a) =>
        {
            if (a.Status != AsyncOperationStatus.Succeeded)
            {
                return;
            }

            data = a.Result;
        };

        return data;
    }
}
