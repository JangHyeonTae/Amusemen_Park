using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ExtractFloor
{
    public static Dictionary<int, List<Vector3Int>> roomFloorDataDic = new Dictionary<int, List<Vector3Int>>();
    

    public static List<Vector3Int> InstItemGenerator(int roomIndex, RandomWalkSO so)
    {
        List<Vector3Int> itemPos = new();

        while (itemPos.Count != so.maxItemCount)
        {
            int index = UnityEngine.Random.Range(0, roomFloorDataDic[roomIndex].Count);

            if (roomFloorDataDic.TryGetValue(roomIndex, out List<Vector3Int> value))
            {
                if (!itemPos.Contains(value[index]))
                    itemPos.Add(value[index]);
            }
        }
        
        return itemPos;
    }

    public static void AddItemPosDic(int roomIndex, HashSet<Vector3Int> posHash)
    {
        ClearItemPosDic();
        List<Vector3Int> list = new();
        
        foreach (var pos in posHash)
        {
            list.Add(pos);
        }

        roomFloorDataDic.Add(roomIndex, list);
    }

    public static void ClearItemPosDic()
    {
        roomFloorDataDic.Clear();
    }

}
