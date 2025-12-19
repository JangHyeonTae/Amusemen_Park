using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestDemo : MonoBehaviour
{
    public QuestSO needPointQuestSO;
    public QuestSO EnemyQuestSO;

    public Button NeedPointQuestAddButton;
    public Button EnemyQuestAddButton;
    public Button needPointFinishQuest;
    public Button enemyfinishQuest;
    public Button countUp;

    public TextMeshProUGUI questText;
    public TextMeshProUGUI questNeedCount;
    public TextMeshProUGUI questCountText;

    private void OnEnable()
    {
        countUp.onClick.AddListener(QuestContinue);
        NeedPointQuestAddButton.onClick.AddListener(() => QuestSetting(needPointQuestSO));
        EnemyQuestAddButton.onClick.AddListener(() => QuestSetting(EnemyQuestSO));
        needPointFinishQuest.onClick.AddListener(() => QuestFaild(needPointQuestSO));
        enemyfinishQuest.onClick.AddListener(() => QuestFaild(EnemyQuestSO));
    }

    private void OnDisable()
    {
        countUp.onClick.RemoveListener(QuestContinue);
        NeedPointQuestAddButton.onClick.RemoveListener(() => QuestSetting(needPointQuestSO));
        EnemyQuestAddButton.onClick.RemoveListener(() => QuestSetting(EnemyQuestSO));
        needPointFinishQuest.onClick.RemoveListener(() => QuestFaild(needPointQuestSO));
        enemyfinishQuest.onClick.RemoveListener(() => QuestFaild(EnemyQuestSO));
    }

    public void QuestContinue()
    {
        QuestManager.Instance.QuestCountUp();
        questCountText.text = InventoryManager.Instance.myNormalPoint.ToString();
    }

    public void QuestSetting(QuestSO so)
    {
        QuestManager.Instance.SpawnQuest(so);
        questText.text = so.questName;
        questNeedCount.text = so.startNeedCount.ToString();
    }

    public void QuestFaild(QuestSO so)
    {
        if (QuestManager.Instance.QuestFinish(so))
        {
            Debug.Log("성공");
        }
        else
        {
            Debug.Log("실패");
        }
    }
}
