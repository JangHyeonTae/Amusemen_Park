using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

public enum Quest
{
    Enemy,
    NeedPoint
}

public class QuestManager : Singleton<QuestManager>
{
    public List<QuestSample> questList;


    public void QuestCountUp()
    {
        InventoryManager.Instance.myNormalPoint++;
    }

    public bool QuestFinish(QuestSO so)
    {
        if (questList == null)
            return false;

        var quest = questList.Find(q => q.quest == so.questType);

        if(quest != null)
        {
            if (quest.questSO.questType == Quest.NeedPoint)
            {
                if (quest.questNeedCount <= InventoryManager.Instance.myNormalPoint)
                {
                    InventoryManager.Instance.myNormalPoint -= quest.questNeedCount;
                    questList.Remove(quest);
                    return true;
                }
            }
        }
        else
        {
            Debug.Log("해당 퀘스트 없음");
            return false;
        }

        Debug.Log("퀘스트 실패");
        return false;
    }

    public void SpawnQuest(QuestSO so)
    {
        if (questList == null)
        {
            questList = new List<QuestSample>();
        }

        questList.Add(new QuestSample(so));
    }
}
