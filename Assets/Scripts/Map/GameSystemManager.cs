using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameSystemManager : Singleton<GameSystemManager>
{
    public int currentStage;
    public Vector3Int startPos;
    public Vector3Int mapEndPos;
    public Vector3Int mapStartPos;

    [SerializeField] private PressFObj startObj;
    [SerializeField] private PressFObj endObj;
    AbstractMap generator;

    protected override void Awake()
    {
        base.Awake();
        if (generator == null)
            generator = FindObjectOfType<AbstractMap>();
    }

    private void Start()
    {
        generator.GenerateMap();

        startPos = new Vector3Int(3, 0, 3);

        PressFObj inst = Instantiate(startObj, startPos, Quaternion.identity);
        PressFObj mapStartObj = Instantiate(startObj, mapStartPos, Quaternion.identity);
        PressFObj mapEndObj = Instantiate(endObj, mapEndPos, Quaternion.identity);
    }

    public void ChangeMap()
    {

    }

    public void MapClear()
    {
        if (InventoryManager.Instance.CurrentPoint() >= QuestManager.Instance.CurrentQuest(0).questNeedCount)
        {
            Debug.Log("성공");
            currentStage++;
        }
        else
        {
            Debug.Log("미션 실패");
        }
    }
}
