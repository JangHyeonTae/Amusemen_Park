using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class InstItem
{
    public static Dictionary<int, List<Vector3Int>> itemPosDic = new Dictionary<int, List<Vector3Int>>();

    public static List<Vector3Int> InstItemGenerator(int roomIndex, RandomWalkSO so)
    {
        List<Vector3Int> itemPos = new();

        while (itemPos.Count != so.maxItemCount)
        {
            int index = UnityEngine.Random.Range(0, itemPosDic[roomIndex].Count);

            if (itemPosDic.TryGetValue(roomIndex, out List<Vector3Int> value))
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

        itemPosDic.Add(roomIndex, list);
    }

    public static void ClearItemPosDic()
    {
        itemPosDic.Clear();
    }

}
