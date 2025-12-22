using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameSystemManager : Singleton<GameSystemManager>
{
    public int currentStage;
    public Vector3Int startPos;
    public Vector3Int mapEndPos;
    public Vector3Int mapStartPos;

    [SerializeField] private StartEndObj startObj;
    AbstractMap generator;

    protected override void Awake()
    {
        base.Awake();
        if (generator == null)
            generator = FindObjectOfType<AbstractMap>();
    }

    private void Start()
    {
        startPos = new Vector3Int(3, 0, 3);
        StartEndObj inst = Instantiate(startObj);
        inst.transform.position = startPos;

        generator.GenerateMap();
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
