using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quest/QuestSO")]
public class QuestSO : ScriptableObject
{
    [field: SerializeField] public string questName { get; private set; }
    [field: SerializeField] public Quest questType { get; private set; }
    [field: SerializeField] public int startNeedCount { get; private set; }
    [field: SerializeField] public int upCount { get; private set; }

}
