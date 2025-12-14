using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGeneration
{
    public static void CreateWall(HashSet<Vector3Int> floorsPos, MapVisualizer visualizer)
    {
        var basicWallPos = FindWallsDirections(floorsPos, Direction3D.cardinalDirectionsList);

        CreateBasicWalls(visualizer, basicWallPos, floorsPos);
    }

    public static void CreateBasicWalls(MapVisualizer visualizer, HashSet<Vector3Int> basicWallPos, HashSet<Vector3Int> floors)
    {
        foreach (var pos in basicWallPos)
        {
            string neighborPos = "";

            visualizer.InstSingleBasicWall(pos);
        }
    }

    public static HashSet<Vector3Int> FindWallsDirections(HashSet<Vector3Int> floorsPos, List<Vector3Int> directionList)
    {
        HashSet<Vector3Int> wallPos = new HashSet<Vector3Int>();

        foreach (var pos in floorsPos)
        {
            foreach (var dir in directionList)
            {
                var neighborPos = pos + dir;

                if (!floorsPos.Contains(neighborPos))
                {
                    wallPos.Add(neighborPos);
                }
            }
        }

        return wallPos;
    }
}
