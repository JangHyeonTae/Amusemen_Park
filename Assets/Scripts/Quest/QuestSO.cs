using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class QuestSO : ScriptableObject
{
    [feild: SerializeField] public string questName { get; private set; }
    [feild: SerializeField] public Quest quest { get; private set; }
    [field: SerializeField] public int startNeedCount { get; private set; }
    [field: SerializeField] public int upCount { get; private set; }

}
