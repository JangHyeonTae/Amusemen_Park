using Cysharp.Threading.Tasks;
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

    public async UniTask<GameObject> GetData(string name)
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(name);
        await handle.Task;

        if (handle.Status != AsyncOperationStatus.Succeeded)
            return null;

        return handle.Result;
    }
}
