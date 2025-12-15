using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SampleMap : AbstractMap
{
    [SerializeField] protected RandomWalkSO so;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector3Int> floorsPos = RunRandomWalk(so, startPos);
        mapVisualizer.Clear();
        mapVisualizer.OrderFloor(floorsPos);

        WallGeneration.CreateWall(floorsPos, mapVisualizer);
    }

    protected HashSet<Vector3Int> RunRandomWalk(RandomWalkSO so, Vector3Int pos)
    {
        var currentPos = pos;
        HashSet<Vector3Int> floorsPos = new HashSet<Vector3Int>();

        for(int i =0; i < so.iterations; i++)
        {
            var path = Algorithm.SimpleRandomWalk(currentPos, so.walkLength);
            floorsPos.UnionWith(path);

            if (so.startRandomlyEachIteration)
            {
                currentPos = floorsPos.ElementAt(UnityEngine.Random.Range(0, floorsPos.Count));
            }
        }


        return floorsPos;
    }
}
