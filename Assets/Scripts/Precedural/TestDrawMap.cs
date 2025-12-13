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
        HashSet<Vector3Int> floorsPos = RunRandomWalk(startPos);
        mapVisualizer.Clear();
        mapVisualizer.OrderFloor(floorsPos);
    }

    private HashSet<Vector3Int> RunRandomWalk(Vector3Int pos)
    {
        var currentPos = pos;
        HashSet<Vector3Int> floorsPos = new HashSet<Vector3Int>();

        for(int i =0; i < iterations; i++)
        {
            var path = Algorithm.SimpleRandomWalk(currentPos, walkLength);
            floorsPos.UnionWith(path);

            if (startRandomlyEachIteration)
            {
                currentPos = floorsPos.ElementAt(UnityEngine.Random.Range(0, floorsPos.Count));
            }
        }


        return floorsPos;
    }
}
