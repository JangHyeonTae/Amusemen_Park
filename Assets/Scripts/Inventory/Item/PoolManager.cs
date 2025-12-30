using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    public SampleItem sampleItem;
    public ObjectPool itemPool;

    public MonsterSample monsterSample;
    public ObjectPool monsterPool;
    protected override void Awake()
    {
        base.Awake();
        CreatePool();
    }

    private void CreatePool()
    {
        itemPool = new ObjectPool(sampleItem, 100, transform);
        monsterPool = new ObjectPool(monsterSample, 40, transform);
    }
}
