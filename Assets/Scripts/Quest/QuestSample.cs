using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestSample
{
    public int questNeedCount;
    public Quest quest;
    public QuestSO questSO;

    public QuestSample(QuestSO so)
    {
        questSO = so;
        questNeedCount = so.startNeedCount;
        quest = so.questType;
    }

    public void UpQuestCount()
    {
        questNeedCount = questNeedCount * questSO.upCount;
    }
}
