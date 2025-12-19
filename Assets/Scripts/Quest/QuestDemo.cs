using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestDemo : MonoBehaviour
{
    public QuestSO questSO;

    public Button addButton;
    public Button finishQuest;

    private void OnEnable()
    {
        addButton.onClick.AddListener(QuestSetting);
        finishQuest.onClick.AddListener(QuestFaild);
    }

    private void OnDisable()
    {
        addButton.onClick.RemoveListener(QuestSetting);
        finishQuest.onClick.RemoveListener(QuestFaild);
    }

    public void QuestSetting()
    {
        QuestManager.Instance.SpawnQuest(questSO);
    }

    public void QuestFaild()
    {
        if (QuestManager.Instance.QuestFinish(questSO))
        {
            Debug.Log("성공");
        }
        else
        {
            Debug.Log("실패");
        }
    }
}
