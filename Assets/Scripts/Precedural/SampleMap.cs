using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SampleMap : AbstractMap
{
    [SerializeField] protected RandomWalkSO randomWalkso;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector3Int> floorsPos = RunRandomWalk(randomWalkso, startPos);

        mapVisualizer.Clear();
        mapVisualizer.OrderFloor(floorsPos);

        WallGeneration.CreateWall(floorsPos, mapVisualizer);
    }

    protected HashSet<Vector3Int> RunRandomWalk(RandomWalkSO so, Vector3Int pos)
    {
        var currentPos = pos;
        HashSet<Vector3Int> floorsPos = new HashSet<Vector3Int>();

        for(int i =0; i < so.iterations + GameSystemManager.Instance.currentStage; i++)
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
