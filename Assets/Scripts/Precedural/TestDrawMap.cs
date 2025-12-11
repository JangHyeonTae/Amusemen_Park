using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestDrawMap : AbstractMap
{
    public int iterations = 10;
    public int walkLength = 10;
    public bool startRandomlyEachIteration = true;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorsPos = RunRandomWalk(startPos);
        mapVisualizer.Clear();
        mapVisualizer.OrderFloor(floorsPos);
    }

    private HashSet<Vector2Int> RunRandomWalk(Vector2Int pos)
    {
        var currentPos = pos;
        HashSet<Vector2Int> floorsPos = new HashSet<Vector2Int>();

        for(int i =0; i < iterations; i++)
        {
            var path = Algorithm.SimpleRandomWalk(pos, walkLength);
            floorsPos.UnionWith(path);

            if (startRandomlyEachIteration)
            {
                currentPos = floorsPos.ElementAt(UnityEngine.Random.Range(0, floorsPos.Count));
            }
        }
        return floorsPos;
    }
}
