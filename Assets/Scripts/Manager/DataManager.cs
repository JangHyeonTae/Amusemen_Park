using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class DataManager : Singleton<DataManager>
{
    private List<AudioClip> audioList;
    private AsyncOperationHandle<IList<AudioClip>> audioHandler;


    protected override void Awake()
    {
        base.Awake();
    }
    private IEnumerator Start()
    {
        yield return null;

        audioList = new();
        audioHandler = Addressables.LoadAssetsAsync<AudioClip>("AudioClips",clip => { audioList.Add(clip); } );

        yield return audioHandler;

        GetAudioData(audioHandler);
    }
    public async UniTask<GameObject> GetData(string name)
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(name);
        await handle.Task;

        if (handle.Status != AsyncOperationStatus.Succeeded)
            return null;

        return handle.Result;
    }

    public void GetAudioData(AsyncOperationHandle<IList<AudioClip>> objs)
    {
        List<AudioClip> list = new();
        foreach (var o in objs.Result)
        {
            audioList.Add(o);
        }
    }

    public AudioClip GetSFX(string name)
    {
        AudioClip data = audioList.Where(a => a.name == name).FirstOrDefault();

        return data;
    }
}
