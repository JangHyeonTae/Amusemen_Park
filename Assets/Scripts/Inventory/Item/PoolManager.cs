using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField] private SampleItem sampleItem;
    [SerializeField] private ItemPanelPrefab itemPanel;
    //private GameObject itemParent;
    ObjectPool pool;

    private void Start()
    {
        //itemParent = Instantiate(new GameObject(), transform);
        //itemParent.name = sampleItem.name;
        pool = new ObjectPool(sampleItem, 30, this.transform, true);
    }

    private void Create()
    {

    }
}
