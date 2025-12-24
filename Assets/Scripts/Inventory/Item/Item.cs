using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    [field : SerializeField] public int itemNum {  get; private set; }
    [field: SerializeField] public string itemName { get; private set; }
    [field: SerializeField] public SpriteRenderer itemIcon {  get; private set; }
    [field: SerializeField] public bool canUse { get; private set; }
    [field: SerializeField] public int cost { get; private set; }
    [field: SerializeField] public EMoney moneyType { get; private set; }
    [field: SerializeField] public GameObject itemVisual {  get; private set; }
    [field : SerializeField] public int weight { get; private set; }
}
