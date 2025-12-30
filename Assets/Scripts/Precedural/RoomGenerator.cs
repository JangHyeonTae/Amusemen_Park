using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class RoomGenerator : SampleMap
{
    [SerializeField] private int minRoomWidth = 4;
    [SerializeField] private int minRoomHeight = 4;
    [SerializeField] private int dungeonWidth = 20;
    [SerializeField] private int dungeonHeight = 20;

    [SerializeField][Range(0, 10)] private int offset = 1;
    [SerializeField] private bool randomWalkRooms = false;

    private List<Vector3Int> posList = new();
    private List<Vector3Int> monsterPosList = new();
    //[Header("Ç®")]
    //private ObjectPool itemPool;

    private void Start()
    {
        //itemPool = new ObjectPool(sampleItem, 100, this.transform, true);
    }

    protected  void OnDisable()
    {
        DestroyItem();
    }

    protected override void RunProceduralGeneration()
    {
        DestroyItem();
        CreateRooms();
    }

    private void CreateRooms()
    {
        var roomsList = Algorithm.BSP(
            new BoundsInt((Vector3Int)startPos, new Vector3Int(dungeonWidth, dungeonHeight, 0)),
            minRoomWidth,
            minRoomHeight);

        HashSet<Vector3Int> floor = new HashSet<Vector3Int>();

        if (randomWalkRooms)
        {
            floor = CreateRoomsRandomly(roomsList);
        }
        else
        {
            floor = CreateSimpleRooms(roomsList);
        }

        List<Vector3Int> roomCenters = new List<Vector3Int>();

        foreach (var room in roomsList)
        {
            roomCenters.Add(Vector3Int.RoundToInt(room.center));
        }

        HashSet<Vector3Int> corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);

        mapVisualizer.OrderFloor(floor);
        WallGeneration.CreateWall(floor, mapVisualizer);
    }

    private HashSet<Vector3Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector3Int> floor = new HashSet<Vector3Int>();

        for(int i =0; i < roomsList.Count; i++)
        {
            for (int col = offset; col < roomsList[i].size.x - offset; col++)
            {
                for (int row = offset; row < roomsList[i].size.y - offset; row++)
                {
                    Vector3Int pos = (Vector3Int)roomsList[i].min + new Vector3Int(col, row);
                    floor.Add(pos);
                }
            }

            ExtractFloor.AddItemPosDic(i, floor);
        }

        return floor;
    }

    private HashSet<Vector3Int> CreateRoomsRandomly(List<BoundsInt> roomsList)
    {
        HashSet<Vector3Int> floor = new HashSet<Vector3Int>();
        List<Vector3Int> intersectList = new List<Vector3Int>();


        for (int i =0; i < roomsList.Count; i++)
        {
            var roomBounds = roomsList[i];
            var roomCenter = new Vector3Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            var roomFloor = RunRandomWalk(randomWalkso, roomCenter);

            foreach (var pos in roomFloor)
            {
                if (pos.x >= (roomBounds.xMin + offset) &&
                    pos.x <= (roomBounds.xMax - offset) &&
                    pos.y >= (roomBounds.yMin + offset) &&
                    pos.y <= (roomBounds.yMax - offset))
                {
                    floor.Add(pos);
                }
            }
            
            ExtractFloor.AddItemPosDic(i, floor);

            posList.Clear();
            posList = ExtractFloor.InstItemGenerator(i, randomWalkso);
            monsterPosList = ExtractFloor.InstItemGenerator(i, randomWalkso);

            intersectList = ExtractFloor.IntersectList(posList);

            for (int index = 0; index < randomWalkso.maxItemCount; index++)
            {
                //SampleItem item = itemPool.GetPooled() as SampleItem;
                //item.Init(itemSO[UnityEngine.Random.Range(0, itemSO.Length)]);

                SampleItem item = PoolManager.Instance.itemPool.GetPooled() as SampleItem;
                item.Init(itemSO[UnityEngine.Random.Range(0, itemSO.Length)]);

                var normalPos = new Vector3Int(posList[index].x, 0, posList[index].y);
                var pos = new Vector3Int(posList[index].x * (int)mapVisualizer.GridSize().x, 0,
                                posList[index].y * (int)mapVisualizer.GridSize().y);
                item.transform.position = pos;
                
                posList.Remove(normalPos);
            }

            for (int index = 0; index < GameSystemManager.Instance.currentStage - 1; index++)
            {
                MonsterSample inst = PoolManager.Instance.monsterPool.GetPooled() as MonsterSample;
                inst.Init();

                var normalPos = new Vector3Int(monsterPosList[index].x, 0, monsterPosList[index].y);
                var pos = new Vector3Int(monsterPosList[index].x * (int)mapVisualizer.GridSize().x, 0,
                                monsterPosList[index].y * (int)mapVisualizer.GridSize().y);
                inst.transform.position = pos;

                monsterPosList.Remove(normalPos);
            }

        }


        if (GameSystemManager.Instance == null)
        {
            Debug.Log("GameSystemManager¾øÀ½");
        }

        List<Vector3Int> SEPosList = new();
        SEPosList = ExtractFloor.roomFloorDataList;

        Vector3Int SPos = PosSet(intersectList, UnityEngine.Random.Range(0, (int)(SEPosList.Count) / 2));
        Vector3Int EPos = PosSet(intersectList, UnityEngine.Random.Range((int)(SEPosList.Count - 1) / 2, SEPosList.Count));
        

        GameSystemManager.Instance.mapStartPos = SPos;
        GameSystemManager.Instance.mapEndPos = EPos;

        return floor;
    }


    private Vector3Int PosSet( List<Vector3Int> posList, int index)
    {
        if (posList == null || posList.Count == 0)
        {
            Debug.LogWarning("PosSet: posList°¡ ºñ¾úÀ½");
            return Vector3Int.zero;
        }

        return new Vector3Int(posList[index].x * (int)mapVisualizer.GridSize().x, 0,
                        posList[index].y * (int)mapVisualizer.GridSize().y);
        
    }

    private HashSet<Vector3Int> ConnectRooms(List<Vector3Int> roomCenters)
    {
        HashSet<Vector3Int> corridors = new HashSet<Vector3Int>();
        var currentRoomCenter = roomCenters[UnityEngine.Random.Range(0, roomCenters.Count)];

        roomCenters.Remove(currentRoomCenter);

        while(roomCenters.Count > 0)
        {
            Vector3Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector3Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
            currentRoomCenter = closest;
            corridors.UnionWith(newCorridor);
        }

        return corridors;
    }

    private Vector3Int FindClosestPointTo(Vector3Int currentRoomCenter, List<Vector3Int> roomCenters)
    {
        Vector3Int closest = Vector3Int.zero;

        float distance = float.MaxValue;

        foreach (var pos in roomCenters)
        {
            float currentDistance = Vector3.Distance(pos, currentRoomCenter);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                closest = pos;
            }
        }
        return closest;
    }


    private HashSet<Vector3Int> CreateCorridor(Vector3Int currentRoomCenter, Vector3Int destination)
    {
        HashSet<Vector3Int> corridor = new HashSet<Vector3Int>();
        var pos = currentRoomCenter;
        corridor.Add(pos);

        while (pos.y != destination.y)
        {
            if (destination.y > pos.y)
            {
                pos += Vector3Int.up;
            }
            else if(destination.y< pos.y)
            {
                pos += Vector3Int.down;
            }
            corridor.Add(pos);
        }

        while (pos.x != destination.x)
        {
            if (destination.x > pos.x)
            {
                pos += Vector3Int.right;
            }
            else if (destination.x < pos.x)
            {
                pos += Vector3Int.left;
            }
            corridor.Add(pos);
        }

        return corridor;
    }

    protected void DestroyItem()
    {
        var itemPool = PoolManager.Instance?.itemPool;
        var itemParent = itemPool?.poolParent?.transform;
        if (itemParent != null)
        {
            for (int i = itemParent.childCount - 1; i >= 0; i--)
            {
                var t = itemParent.GetChild(i);
                var item = t.GetComponent<SampleItem>();
                if (item == null) continue;     // ÇÙ½É: ¼¯ÀÎ ¾Öµé ½ºÅµ
                item.Release();
            }
        }

        var monsterPool = PoolManager.Instance?.monsterPool;
        var monsterParent = monsterPool?.poolParent?.transform;
        if (monsterParent != null)
        {
            for (int i = monsterParent.childCount - 1; i >= 0; i--)
            {
                var t = monsterParent.GetChild(i);
                var m = t.GetComponent<MonsterSample>();
                if (m == null) continue;
                m.Release();
            }
        }
    }
}
