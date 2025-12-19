using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public enum Quest
{
    Enemy,
    NeedPoint
}

public class QuestSystem : MonoBehaviour
{
    public Quest quest;
    public QuestSample questSample;

    //private bool QuestFinish()
    //{
    //    if (questSample.questSO.quest == Quest.NeedPoint)
    //    {
    //        if (questSample.questNeedCount <= InventoryManager.Instance.myNormalPoint)
    //        {
    //            InventoryManager.Instance.myNormalPoint -= questSample.questNeedCount;
    //            return true;
    //        }
    //    }
    //    else if (questSample.questSO.quest == Quest.Enemy)
    //    {
    //        if(queestSample.questNeedCount <= )

    //    }
    //    Debug.Log("Äù½ºÆ® ½ÇÆÐ");
    //    return false;
    //}

    public void SpawnQuest(QuestSO so)
    {
        if (questSample != null)
        {
            questSample = null;
        }

        questSample = new QuestSample(so);
    }
}
