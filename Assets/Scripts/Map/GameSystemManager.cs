using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameSystemManager : Singleton<GameSystemManager>
{
    public int currentStage;
    public Vector3Int startPos;
    public Vector3Int mapEndPos;
    public Vector3Int mapStartPos;

    [SerializeField] private PressFObj startObj;
    [SerializeField] private PressFObj endObj;
    AbstractMap generator;

    PressFObj inst;
    public PressFObj mapStartObj;
    public PressFObj mapEndObj;

    [SerializeField] private AudioClip audioClip1;
    protected override void Awake()
    {
        base.Awake();
        if (generator == null)
            generator = FindObjectOfType<AbstractMap>();
    }


    public async UniTask ChangeMap()
    {
        if (currentStage == 2)
        {
            AudioManager.Instance.PlayeSFX(audioClip1);
        }

        ExtractFloor.ClearAll();
        generator.GenerateMap();

        startPos = new Vector3Int(3, 0, 3);

        if (startObj == null)
        {
            GameObject go = await DataManager.Instance.GetData("StartPos");
            startObj = go.GetComponent<PressFObj>();
        }

        if (endObj == null)
        {
            GameObject go = await DataManager.Instance.GetData("EndPos");
            endObj = go.GetComponent<PressFObj>();
        }

        inst = Instantiate(startObj, startPos, Quaternion.identity);
        mapStartObj = Instantiate(startObj, mapStartPos, Quaternion.identity);
        mapEndObj = Instantiate(endObj, mapEndPos, Quaternion.identity);

        currentStage++;

        if(currentStage != 0)
            InventoryManager.Instance.maxWeight += (int)(currentStage * 2f);
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(LoadAndInit(sceneName));
    }

    private IEnumerator LoadAndInit(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        if (inst != null)
            Destroy(inst);
        if (mapStartObj != null)
            Destroy(mapStartObj);
        if (mapEndObj != null)
            Destroy(mapEndObj);
        
        yield return null;

        if (sceneName == "Map")
        {
            generator = FindObjectOfType<AbstractMap>();
            ChangeMap().Forget();
        }
    }
}
